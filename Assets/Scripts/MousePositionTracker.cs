using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionTracker : MonoBehaviour
{
  private Camera mainCamera;
  void Start()
  {
    mainCamera = Camera.main;
  }

  public Vector3 GetMouseWorldPosition()
  {
    Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    mouseWorldPosition.z = 0;
    return mouseWorldPosition;
  }
}
