using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherController : MonoBehaviour
{
  private Transform launcherTransform;
  private Transform launchPointTransform;
  [SerializeField] private GameObject rocketPrefab;
  [SerializeField] private GameObject rocketTargetPrefab;

  private void Awake()
  {
    launcherTransform = transform.Find("Launcher");
    launchPointTransform = launcherTransform.Find("LaunchPoint");
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Fire();
    }

    HandleLauncherRotation();
  }

  private void Fire()
  {
    GameObject target = Instantiate(rocketTargetPrefab, MousePositionTracker.Instance.GetMouseWorldPosition(), Quaternion.identity);
    GameObject missile = Instantiate(rocketPrefab, launchPointTransform.position, launcherTransform.rotation);
    missile.GetComponent<RocketController>().SetTarget(target);
  }

  private void HandleLauncherRotation()
  {
    Vector3 mouseWorldPosition = MousePositionTracker.Instance.GetMouseWorldPosition();
    float angleDeg = AngleMath.VectorAngle(mouseWorldPosition - launcherTransform.position);
    launcherTransform.eulerAngles = new Vector3(0, 0, angleDeg);
  }
}
