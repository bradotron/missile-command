using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherController : MonoBehaviour
{
  private Transform launcherTransform;
  private Transform launchPointTransform;
  [SerializeField] private GameObject missilePrefab;

  private void Awake()
  {
    launcherTransform = transform.Find("Launcher");
    launchPointTransform = launcherTransform.Find("LaunchPoint");
  }

  void Start()
  {

  }

  // Update is called once per frame
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
    GameObject missile = Instantiate(missilePrefab, launchPointTransform.position, launcherTransform.rotation);
    missile.GetComponent<RocketController>().SetTarget(MousePositionTracker.Instance.GetMouseWorldPosition());
  }

  private void HandleLauncherRotation()
  {
    Vector3 mouseWorldPosition = MousePositionTracker.Instance.GetMouseWorldPosition();
    float angleDeg = AngleMath.VectorAngle(mouseWorldPosition - launcherTransform.position);
    launcherTransform.eulerAngles = new Vector3(0, 0, angleDeg);
  }
}
