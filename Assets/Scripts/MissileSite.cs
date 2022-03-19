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
    Vector2 directionToMouse = (mouseWorldPosition - transform.position).normalized;
    float angleDeg = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
    launcherTransform.eulerAngles = new Vector3(0, 0, angleDeg);
  }
}
