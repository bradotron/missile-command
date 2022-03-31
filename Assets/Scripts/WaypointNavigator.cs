using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WaypointNavigator : MonoBehaviour
{
  private Rigidbody2D rb2d;
  private float targetSpeed;

  [SerializeField]
  private Vector2[] waypointsArray;
  private Queue<Vector2> waypointsQueue;
  private Vector2 currentWaypoint;

  public void SetTargetSpeed(float speed)
  {
    targetSpeed = speed;
  }

  public void SetRigidBody2d(Rigidbody2D rb2d)
  {
    this.rb2d = rb2d;
  }

  private void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    waypointsQueue = new Queue<Vector2>();
    foreach (Vector2 waypoint in waypointsArray)
    {
      waypointsQueue.Enqueue(waypoint);
    }
  }

  private void Start()
  {
    StartNavigationToNextWaypoint();
  }

  void Update()
  {
    float distanceToWaypoint = (currentWaypoint - rb2d.position).magnitude;
    // if within some distance of current waypoint, set next waypoint as current
    if (distanceToWaypoint <= 0.5f)
    {
      StartNavigationToNextWaypoint();
    }
  }

  private void StartNavigationToNextWaypoint()
  {
    SetNextWaypoint();
    SetNewVelocity();
  }

  private void SetNewVelocity()
  {
    Vector2 dirToWaypoint = (currentWaypoint - rb2d.position).normalized;
    rb2d.velocity = dirToWaypoint * targetSpeed;
  }

  private void SetNextWaypoint()
  {
    currentWaypoint = waypointsQueue.Dequeue();
    waypointsQueue.Enqueue(currentWaypoint);
  }
}
