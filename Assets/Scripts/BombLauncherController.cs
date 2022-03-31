using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLauncherController : MonoBehaviour
{
  [SerializeField] private GameObject pfBomb;
  [SerializeField] private float bombReloadTime;
  [SerializeField] private bool safety;
  private Transform bombLaunchPointTransform;
  private WaypointNavigator waypointNavigator;
  private bool bombLoaded;
  private float reloadTimer;
  [SerializeField] private float bomTargetXMin = -18f;
  [SerializeField] private float bomTargetXMax = 18f;
  [SerializeField] private float bombTargetY = -5.5f;
  [SerializeField] private float bombSpeed = 5f;

  private void Awake()
  {
    waypointNavigator = GetComponent<WaypointNavigator>();
    waypointNavigator.SetTargetSpeed(5f);
    bombLaunchPointTransform = transform.Find("BombLaunchPoint");
    bombLoaded = false;
    reloadTimer = 0f;
  }

  void Update()
  {
    if (bombLoaded)
    {
      if (!safety)
      {
        LaunchBomb();
      }
    }
    else
    {
      HandleBombReload();
    }
  }

  private void HandleBombReload()
  {
    reloadTimer += Time.deltaTime;
    if (reloadTimer >= bombReloadTime)
    {
      bombLoaded = true;
    }
  }

  private void LaunchBomb()
  {
    GameObject bomb = Instantiate(pfBomb, bombLaunchPointTransform.position, Quaternion.identity);
    Rigidbody2D bombRb2d = bomb.GetComponent<Rigidbody2D>();
    SetBombRotationAndVelocity(bombRb2d);

    bombLoaded = false;
    reloadTimer = 0f;
  }

  private void SetBombRotationAndVelocity(Rigidbody2D bombRb2d)
  {
    Vector2 bombTarget = GetBombTargetPoint();
    Vector2 direction = (bombTarget - (Vector2)transform.position).normalized;
    bombRb2d.MoveRotation(AngleMath.FindAngle(direction));
    bombRb2d.velocity = direction * bombSpeed;
  }

  private Vector2 GetBombVelocity()
  {
    Vector2 direction = (GetBombTargetPoint() - (Vector2)transform.position).normalized;
    return direction * bombSpeed;
  }


  private Vector2 GetBombTargetPoint()
  {
    float x = UnityEngine.Random.Range(bomTargetXMin, bomTargetXMax);
    float y = bombTargetY;

    Debug.DrawLine(bombLaunchPointTransform.position, new Vector2(x, y), Color.red, 3f);

    return new Vector2(x, y);
  }
}
