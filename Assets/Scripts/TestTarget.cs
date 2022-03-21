using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour
{
  private WaypointNavigator waypointNavigator;

  private void Awake()
  {
    waypointNavigator = GetComponent<WaypointNavigator>();
    waypointNavigator.SetRigidBody2d(GetComponent<Rigidbody2D>());
    waypointNavigator.SetTargetSpeed(5f);
  }

}
