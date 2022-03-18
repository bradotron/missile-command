using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCursor : MonoBehaviour
{
  private MousePositionTracker mousePositionTracker;
  void Start()
  {
    mousePositionTracker = GetComponent<MousePositionTracker>();
  }

  void Update()
  {
    transform.position = mousePositionTracker.GetMouseWorldPosition();
  }
}
