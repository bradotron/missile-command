using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLauncherController : MonoBehaviour
{
  [SerializeField] private GameObject pfBomb;
  [SerializeField] private float bombReloadTime;
  [SerializeField] private bool safety;
  private Vector2 bombLaunchPoint;
  public bool bombLoaded;
  public float reloadTimer;

  private void Awake()
  {
    bombLaunchPoint = transform.Find("BombLaunchPoint").position;
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
    GameObject bomb = Instantiate(pfBomb, bombLaunchPoint, Quaternion.identity);
    bomb.GetComponent<BombController>().SetVelocity(new Vector2(0, -5f));

    bombLoaded = false;
    reloadTimer = 0f;
  }
}
