using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
  [SerializeField] private GameObject pfExplosion;
  private GameObject target;
  private Vector2 targetPosition;
  private Rigidbody2D rb2d;
  private MissileData missileData;

  private void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    missileData = new MissileData() { speed = 5f };
  }

  private void Start()
  {
    rb2d.velocity = transform.right * missileData.speed;
  }

  private void Update()
  {
    Vector2 toTarget = targetPosition - (Vector2)transform.position;
    if (toTarget.magnitude < 0.1)
    {
      Explode();
    }
  }

  public void SetTarget(GameObject target)
  {
    this.target = target;
    targetPosition = this.target.GetComponent<Transform>().position;
  }

  private void Explode()
  {
    // spawn explosion
    Instantiate(pfExplosion, transform.position, Quaternion.identity);
    // destroy self and target marker
    Destroy(gameObject);
    Destroy(target);
  }
}
