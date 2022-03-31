using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BombController : MonoBehaviour
{
  private Rigidbody2D rb2d;
  // Start is called before the first frame update

  private void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
  }

  public void SetVelocity(Vector2 velocity)
  {
    rb2d.velocity = velocity;
  }

  private void OnTriggerEnter2D(Collider2D collider2d)
  {
    Explode();
  }

  private void Explode()
  {
    Debug.Log("Boom!");
    Destroy(gameObject);
  }
}
