using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

  [SerializeField] private Transform targetTransform;

  private Rigidbody2D rb2d;

  private string myVelocityTag = "myVelocity";
  private string toTargetTag = "toTarget";

  private void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    InitializeMyVelocity();
    InitializetoTarget();
  }

  private void Update()
  {
    UpdateMyVelocity();
    UpdatetoTarget();
  }

  // physics goes here
  private void FixedUpdate()
  {
    // need to calculate point of intercept / where to go
    Vector3 pointOfIntercept = targetTransform.position;

    // rotate towards pointOfIntercept
    Vector3 dirToRotateTo = (pointOfIntercept - transform.position).normalized;
    float goalAngle = AngleMath.VectorAngle(dirToRotateTo);
    float currentAngle = rb2d.rotation;
    float rotationSpeed = 30f; // deg per second
    float rotationDirection = Mathf.DeltaAngle(currentAngle, goalAngle) > 0 ? 1f : -1f;
    float nextAngle = currentAngle + (rotationDirection * rotationSpeed * Time.fixedDeltaTime);
    rb2d.MoveRotation(nextAngle);

    // thrust
    Thrust();
  }

  private void Thrust()
  {
    rb2d.AddForce(transform.right * 0.5f, ForceMode2D.Force);
  }

  private void UpdateMyVelocity()
  {
    Vector3 start = transform.position;
    Vector3 end = transform.position + (Vector3)rb2d.velocity;
    LineContainer.Instance.UpdateLine(myVelocityTag, start, end);
  }

  private void InitializeMyVelocity()
  {
    Color myVelocityColor = new Color(0, 255, 68);
    Vector3 start = transform.position;
    Vector3 end = transform.position + (Vector3)rb2d.velocity;
    LineContainer.Instance.AddLine(myVelocityTag, start, end, 0.1f, myVelocityColor);
  }
  private void UpdatetoTarget()
  {
    Vector3 dirToTarget = (targetTransform.position - transform.position).normalized * 2f;
    Vector3 start = transform.position;
    Vector3 end = transform.position + dirToTarget;
    LineContainer.Instance.UpdateLine(toTargetTag, start, end);
  }

  private void InitializetoTarget()
  {
    Color toTargetColor = new Color(255, 255, 0);
    Vector3 dirToTarget = (targetTransform.position - transform.position).normalized * 2f;
    Vector3 start = transform.position;
    Vector3 end = transform.position + dirToTarget;
    LineContainer.Instance.AddLine(toTargetTag, start, end, 0.1f, toTargetColor);
  }
}
