using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionTracker : MonoBehaviour
{
  public Camera mainCamera;
  public Vector3 mousePosition { get; private set; }

  private void Update()
  {
    mousePosition = GetMouseWorldPosition();
  }

  public Vector3 GetMouseWorldPosition()
  {
    Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    mouseWorldPosition.z = 0;
    return mouseWorldPosition;
  }
}
