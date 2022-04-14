using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BombController : MonoBehaviour
{
  [SerializeField] private GameObject pfExplosion;
  private HealthSystem healthSystem;
  private Rigidbody2D rb2d;

  private void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    healthSystem = GetComponent<HealthSystem>();
    Dictionary<DamageType, float> vulnerabilities = new Dictionary<DamageType, float>();
    vulnerabilities.Add(DamageType.Explosive, 1f);
    healthSystem.SetDamageVulnerabilities(vulnerabilities);
    healthSystem.OnDied += HealthSystem_OnDied;
  }

  private void HealthSystem_OnDied(object sender, EventArgs e)
  {
    Explode();
  }

  public void SetVelocity(Vector2 velocity)
  {
    rb2d.velocity = velocity;
  }

  // private void OnTriggerEnter2D(Collider2D collider2d)
  // {
  //   Explode();
  // }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    Explode();
  }

  private void Explode()
  {
    Instantiate(pfExplosion, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
