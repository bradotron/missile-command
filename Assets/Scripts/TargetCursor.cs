using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCursor : MonoBehaviour
{
  void Update()
  {
    transform.position = MousePositionTracker.Instance.GetMouseWorldPosition();
  }
}
