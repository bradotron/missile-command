using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionTracker
{
  private Camera mainCamera;

  private static MousePositionTracker instance;
  public static MousePositionTracker Instance
  {
    get
    {
      if (instance == null)
      {
        instance = new MousePositionTracker();
        instance.SetMainCamera();
      }
      return instance;
    }
  }

  private void SetMainCamera()
  {
    if (mainCamera == null)
    {
      mainCamera = Camera.main;
    }
  }

  public Vector3 GetMouseWorldPosition()
  {
    Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    mouseWorldPosition.z = 0;
    return mouseWorldPosition;
  }
}
