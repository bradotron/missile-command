using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncherController : MonoBehaviour
{
  public ProjectileLauncherData launcherData;
  public BaseBrain aimingBrain;
  public BaseBrain shootingBrain;


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
