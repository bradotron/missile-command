using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCursor : MonoBehaviour
{
  private Camera main;

  private void Start()
  {
    main = Camera.main;
  }
  void Update()
  {
    Vector3 newPosition = main.ScreenToWorldPoint(Input.mousePosition);
    newPosition.z = 0;
    transform.position = newPosition;
  }
}
