using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shooting Brain/Human Controlled")]
public class HumanControlledShootingBrain : BaseBrain
{
  public KeyBindingReference FireKeyBindingReference;

  private AbstractProjectileLauncherData launcherData;
  private bool isLoaded = true;
  private bool isFiring = false;
  private float timeUntilLoaded = 0f;

  public override void Initialize(ProjectileLauncherController launcherController)
  {
    this.launcherData = launcherController.launcherData;
  }

  // Update is called once per frame
  public override void Think(ProjectileLauncherController launcherController)
  {
    if (Input.GetKeyDown(FireKeyBindingReference.Value))
    {
      isFiring = true;
    }
    if (Input.GetKeyUp(FireKeyBindingReference.Value))
    {
      isFiring = false;
    }

    if (!isLoaded)
    {
      timeUntilLoaded -= Time.deltaTime;
      if (timeUntilLoaded <= 0f)
      {
        isLoaded = true;
        Debug.Log("Reload Complete");
      }
    }
    else
    {
      if (isFiring)
      {
        // fire an event...OnFire or something
        // then in the projectilelaunchercontroller connect that event to a handler, the handler will connect to the ProjectileLauncher.Fire (this doesn't exist yet, create a SO and add it)
        // Transform projectile = Instantiate(launcherController.selectedProjectile.prefab, launcherController.projectileSpawnPoint.position, launcherController.transform.rotation);
        // Rigidbody2D projectileRb2d = projectile.GetComponent<Rigidbody2D>();
        // projectileRb2d.AddForce(projectile.transform.right * launcherController.selectedProjectile.ProjectileData.launchForce);
        
        isLoaded = false;
        timeUntilLoaded = launcherData.ReloadTime.Value;
      }
    }
  }
}
