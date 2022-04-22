using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncherController : MonoBehaviour
{
  public KeyCode FireKey;
  public ProjectileLauncherData launcherData;
  public AimingBrain aimingBrain;

  private bool isLoaded = true;
  private float timeUntilLoaded = 0f;

  private void Start()
  {
    aimingBrain.Initialize(this);
  }

  // Update is called once per frame
  void Update()
  {
    aimingBrain.Think(this);

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
      if (Input.GetKeyDown(FireKey))
      {
        Debug.Log("Fire!");
        isLoaded = false;
        timeUntilLoaded = launcherData.ReloadTime.Value;
      }
    }
  }
}
