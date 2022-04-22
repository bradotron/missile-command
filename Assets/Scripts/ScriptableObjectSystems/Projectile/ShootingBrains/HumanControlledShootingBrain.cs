using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shooting Brain/Human Controlled")]
public class HumanControlledShootingBrain : BaseBrain
{
  public KeyBindingReference FireKeyBindingReference;

  private ProjectileLauncherData launcherData;
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
        Debug.Log("Fire!");
        isLoaded = false;
        timeUntilLoaded = launcherData.ReloadTime.Value;
      }
    }
  }
}