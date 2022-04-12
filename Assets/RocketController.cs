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
    rb2d.velocity = transform.right * missileData.speed;
  }

  private void Update()
  {
    Vector2 toTarget = targetPosition - (Vector2)transform.position;
    if (toTarget.magnitude < 0.1)
    {
      Destroy(gameObject);
    }
  }

  public void SetTarget(Vector2 target)
  {
    targetPosition = target;
  }
}
