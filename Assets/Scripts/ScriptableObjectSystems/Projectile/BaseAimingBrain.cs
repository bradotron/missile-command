using UnityEngine;

// brains must be created by Create/C# Script
public abstract class AimingBrain : ScriptableObject
{
  public virtual void Initialize(ProjectileLauncherController launcherController) { }
  public abstract void Think(ProjectileLauncherController launcherController);
}
