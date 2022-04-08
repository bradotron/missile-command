using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
  [SerializeField] private ExplosionSO explosion;
  [SerializeField] private float startScale = 0f;
  [SerializeField] private float endScale = 2f;
  [SerializeField] private float explosionTime = 1f;
  private float currentTime = 0f;

  private void Awake()
  {
    transform.localScale = new Vector3(startScale, startScale, startScale);
  }

  private void Update()
  {
    currentTime += Time.deltaTime;
    if (currentTime >= explosionTime)
    {
      Destroy(gameObject);
    }
    float currentScale = endScale * (currentTime / explosionTime);
    transform.localScale = new Vector3(currentScale, currentScale, currentScale);
  }

  void OnTriggerEnter2D(Collider2D col)
  {
    Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
  }
}
