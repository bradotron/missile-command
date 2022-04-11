using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
  private Vector2 targetPosition;
  private Rigidbody2D rb2d;
  private MissileData missileData;

  private void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    missileData = new MissileData() { speed = 5f };
  }

  private void Start()
  {
    Vector2 velocity = (targetPosition - (Vector2)transform.position).normalized * missileData.speed;
    rb2d.velocity = velocity;

  }

  private void Update()
  {
    Vector2 toTarget = targetPosition - (Vector2)transform.position;
    if (toTarget.magnitude < 0.1)
    {
      Debug.Log("Boom!");
      Destroy(gameObject);
    }
  }

  public void SetTarget(Vector2 target)
  {
    targetPosition = target;
  }
}
