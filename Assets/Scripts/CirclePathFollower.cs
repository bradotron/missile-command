using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePathFollower : MonoBehaviour
{
  [SerializeField] private Vector2 center;
  [SerializeField] private float radius;
  [SerializeField] private float timeForOneCircle;

  private float timer = 0f;

  private void Start()
  {
    transform.position = GetPosition();
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = GetPosition();
    UpdateTimer();
  }

  private Vector2 GetPosition()
  {
    Vector2 newPosition = new Vector2();
    float t = GetT();
    newPosition.x = center.x + radius * Mathf.Cos(t);
    newPosition.y = center.y + radius * Mathf.Sin(t);

    return newPosition;
  }

  private float GetT()
  {
    return 2 * Mathf.PI * timer / timeForOneCircle;
  }

  private void UpdateTimer()
  {
    timer += Time.deltaTime;
    if (timer >= timeForOneCircle)
    {
      timer = 0f;
    }
  }
}
