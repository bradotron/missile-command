using UnityEngine;
public abstract class BaseBrain : ScriptableObject
{
  public virtual void Initialize(ProjectileLauncherController launcherController) { }
  public abstract void Think(ProjectileLauncherController launcherController);
}
