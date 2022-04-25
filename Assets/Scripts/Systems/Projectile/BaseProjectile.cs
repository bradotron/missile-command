using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : ScriptableObject
{
  public Transform prefab;
  public BaseProjectileData projectileData;
  // all projectiles have some time to live, and must do something when that is 0
  public abstract void OnExpired(GameObject self);

  // all projectiles must do something when they collide with something
  public abstract void OnCollision(GameObject self, Collider2D collider2D);
}
