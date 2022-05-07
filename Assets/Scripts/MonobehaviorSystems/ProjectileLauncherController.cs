using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncherController : MonoBehaviour
{
  public AbstractProjectileLauncherData launcherData;
  public BaseBrain aimingBrain;
  public BaseBrain shootingBrain;

  public Projectile selectedProjectile;
  public Transform projectileSpawnPoint;

  private void Start()
  {
    aimingBrain.Initialize(this);
    shootingBrain.Initialize(this);
  }

  // Update is called once per frame
  void Update()
  {
    aimingBrain.Think(this);
    shootingBrain.Think(this);
  }
}
