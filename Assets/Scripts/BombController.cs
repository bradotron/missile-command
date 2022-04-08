using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BombController : MonoBehaviour
{
  [SerializeField] private GameObject pfExplosion;
  private Rigidbody2D rb2d;

  private void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
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
    Debug.Log("Explode");
    Instantiate(pfExplosion, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
