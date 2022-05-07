using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : ScriptableObject
{
  public Transform prefab;
  public BaseProjectileData ProjectileData;

  // a guided projectile would implement some version of this
  // public void Think(ProjectileController controller) { }

  // // all projectiles have some time to live, and must do something when that is 0
  public abstract void OnExpired(GameObject self);

  // // all projectiles must do something when they collide with something
  public abstract void OnCollision(GameObject self, Collider2D collider2D);
}
