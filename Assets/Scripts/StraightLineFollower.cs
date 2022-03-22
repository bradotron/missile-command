using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineFollower : MonoBehaviour
{
  [SerializeField] private Vector2 start;
  [SerializeField] private float speed;

  private float timer = 0f;

  private void Start()
  {
    transform.position = start;
  }

  // Update is called once per frame
  void Update()
  {
    transform.position += transform.right * speed * Time.deltaTime;
  }
}
