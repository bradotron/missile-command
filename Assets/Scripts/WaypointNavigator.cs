using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
  private Rigidbody2D rb2d;
  private float targetSpeed;

  [SerializeField]
  private Transform[] waypointsArray;
  private Queue<Transform> waypointsQueue;
  private Transform currentWaypoint;

  public void SetTargetSpeed(float speed)
  {
    targetSpeed = speed;
  }
  public void SetRigidBody2d(Rigidbody2D rb2d)
  {
    this.rb2d = rb2d;
  }

  void Start()
  {
    waypointsQueue = new Queue<Transform>();
    foreach (Transform waypoint in waypointsArray)
    {
      waypointsQueue.Enqueue(waypoint);
    }
    SetNextWaypoint();
  }

  void Update()
  {
    float distanceToWaypoint = ((Vector2)currentWaypoint.position - rb2d.position).magnitude;
    // if within some distance of current waypoint, set next waypoint as current
    if (distanceToWaypoint <= 0.5f)
    {
      SetNextWaypoint();
    }

    Vector2 vNorm = rb2d.velocity.normalized;
    Vector2 dirToWaypoint = ((Vector2)currentWaypoint.position - rb2d.position).normalized;

    if (vNorm != dirToWaypoint)
    {
      rb2d.velocity = dirToWaypoint * targetSpeed;
    }
  }

  private void SetNextWaypoint()
  {
    currentWaypoint = waypointsQueue.Dequeue();
    waypointsQueue.Enqueue(currentWaypoint);
  }
}
