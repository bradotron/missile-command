using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSite : MonoBehaviour
{
  private Transform launcherTransform;

  void Start()
  {
    launcherTransform = transform.Find("Launcher");
  }

  void Update()
  {
    HandleLauncherRotation();
  }

  private void HandleLauncherRotation()
  {
    Vector3 mouseWorldPosition = MousePositionTracker.Instance.GetMouseWorldPosition();
    float angleDeg = AngleMath.VectorAngle(mouseWorldPosition - launcherTransform.position);
    launcherTransform.eulerAngles = new Vector3(0, 0, angleDeg);
  }
}
