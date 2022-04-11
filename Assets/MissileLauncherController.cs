using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherController : MonoBehaviour
{
  private Vector2 launchPoint;
  private Transform launcherTransform;
  [SerializeField] private GameObject missilePrefab;

  private void Awake()
  {
    launcherTransform = transform.Find("Launcher");
    launchPoint = launcherTransform.Find("LaunchPoint").position;
    Debug.Log(launchPoint);
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
    GameObject missile = Instantiate(missilePrefab, launchPoint, Quaternion.identity);
    missile.GetComponent<RocketController>().SetTarget(MousePositionTracker.Instance.GetMouseWorldPosition());
  }

  private void HandleLauncherRotation()
  {
    Vector3 mouseWorldPosition = MousePositionTracker.Instance.GetMouseWorldPosition();
    float angleDeg = AngleMath.VectorAngle(mouseWorldPosition - launcherTransform.position);
    launcherTransform.eulerAngles = new Vector3(0, 0, angleDeg);
  }
}
