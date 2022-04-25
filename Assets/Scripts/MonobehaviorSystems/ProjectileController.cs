using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
  public BaseProjectile projectile;
  private float timeToExpire;
  // Start is called before the first frame update
  void Start()
  {
    // projectile initial velocity comes from launcher or from self???
    timeToExpire = projectile.projectileData.maxFlightTime;
  }

  private void Update()
  {
    timeToExpire -= Time.deltaTime;
    if (timeToExpire <= 0f)
    {
      projectile.OnExpired(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D collider2D)
  {
    projectile.OnCollision(gameObject, collider2D);
  }

}
