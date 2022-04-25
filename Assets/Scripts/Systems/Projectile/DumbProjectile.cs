using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Dumb Projectile")]
public class DumbProjectile : BaseProjectile
{
  public override void OnExpired(GameObject self)
  {
    Destroy(self);
  }
  public override void OnCollision(GameObject self, Collider2D collider2D)
  {
    // do some damage amount to what this hits
    // if(pierces > 0) {
    // then pierces - 1
    // else, destroy self
    Debug.Log("Hit");
    // Destroy(self);
  }
}
