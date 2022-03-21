using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

  [SerializeField] private Transform targetTransform;

  private Rigidbody2D rb2d;

  [SerializeField]
  private float maxAngularVelocity = 60f;

  [SerializeField]
  private float torqueFactor = 1f;

  private PID rotationController;

  [SerializeField]
  [Range(-10, 10)]
  private float rotationControllerKp, rotationControllerKi, rotationControllerKd;

  private PID angularVelocityController;
  [SerializeField]
  [Range(-10, 10)]
  private float angularVelocityControllerKp, angularVelocityControllerKi, angularVelocityControllerKd;


  private string myVelocityTag = "myVelocity";
  private string toTargetTag = "toTarget";
  private string forwardLineTag = "forward";

  [SerializeField]
  private Material pinkMaterial;
  [SerializeField]
  private Material greenMaterial;
  [SerializeField]
  private Material redMaterial;

  private float interceptTime = 0f;
  private bool isThrusting;

  private PID alphaThrustController;
  [SerializeField]
  [Range(-10, 10)]
  private float alphaThrustControllerKp, alphaThrustControllerKi, alphaThrustControllerKd;
  public float alpha;
  public float velocity;
  public float angularVelocity;
  public float minSeenAngularVelocity = 0f;
  public float maxSeenAngularVelocity = 0f;

  private void Awake()
  {
    angularVelocityController = new PID(angularVelocityControllerKp, angularVelocityControllerKi, angularVelocityControllerKd);
    rotationController = new PID(rotationControllerKp, rotationControllerKi, rotationControllerKd);
    alphaThrustController = new PID(alphaThrustControllerKp, alphaThrustControllerKi, alphaThrustControllerKd);
    rb2d = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    InitializeMyVelocity();
    InitializetoTarget();
    InitializeForward();
  }

  private void Update()
  {
    angularVelocity = rb2d.angularVelocity;
    velocity = rb2d.velocity.magnitude;

    HandleLineUpdates();

    UpdatePIDTerms();

    if (Input.GetKeyDown(KeyCode.T))
    {
      isThrusting = true;
    }

    if (Input.GetKeyUp(KeyCode.T))
    {
      isThrusting = false;
    }
  }


  private void HandleLineUpdates()
  {
    UpdateMyVelocity();
    UpdatetoTarget();
    UpdateForward();
  }
  private void UpdatePIDTerms()
  {
    angularVelocityController.Kp = angularVelocityControllerKp;
    angularVelocityController.Ki = angularVelocityControllerKi;
    angularVelocityController.Kd = angularVelocityControllerKd;

    rotationController.Kp = rotationControllerKp;
    rotationController.Ki = rotationControllerKi;
    rotationController.Kd = rotationControllerKd;
  }

  private void FixedUpdate()
  {
    HandleTorque(Time.fixedDeltaTime);

    if (isThrusting)
    {
      Thrust();
    }

    // if the nose direction is different than the velocity, add some force perpendicular to the nose direction
    alpha = Vector2.SignedAngle(rb2d.velocity, transform.right);
    float testCorrection = 1 - Vector2.Dot(transform.right, rb2d.velocity.normalized);
    rb2d.AddForce(transform.up * testCorrection * rb2d.velocity.magnitude);
  }

  private void HandleTorque(float deltaTime)
  {
    // get the rotationController output (rotation is just the rotation about z)
    float losToTarget = AngleMath.VectorAngle(targetTransform.position - transform.position);
    float angleError = Mathf.DeltaAngle(rb2d.rotation, losToTarget);
    float torqueCorrectionForAngle = rotationController.GetOutput(angleError, deltaTime);

    // los rate = (current - previous) / deltaTime. want this = 0;
    float currentLos = AngleMath.VectorAngle(targetTransform.position - transform.position);

    // get the angularVelocity controller output
    // angular velocity controller wants to drive the angular velocity to 0...i.e. the Set Point is 0, so the error is always -angularVelocity
    float angularVelocityError = 0f - rb2d.angularVelocity;
    float torqueCorrectionForAnglularVelocity = angularVelocityController.GetOutput(angularVelocityError, deltaTime);

    float maxForce = maxAngularVelocity * Mathf.Deg2Rad * rb2d.inertia;
    float force = Mathf.Clamp(torqueFactor * (torqueCorrectionForAngle + torqueCorrectionForAnglularVelocity), -maxForce, maxForce);
    rb2d.AddTorque(force);
  }

  private void Thrust()
  {
    rb2d.AddForce(transform.right * 3f, ForceMode2D.Force);
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
    LineContainer.Instance.AddLine(myVelocityTag, start, end, 0.1f, myVelocityColor, redMaterial);
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
    LineContainer.Instance.AddLine(toTargetTag, start, end, 0.1f, toTargetColor, pinkMaterial);
  }

  private void InitializeForward()
  {
    Color toTargetColor = new Color(255, 0, 0);
    Vector3 start = transform.position;
    Vector3 end = transform.position + (transform.right * 3f);
    LineContainer.Instance.AddLine(forwardLineTag, start, end, 0.1f, toTargetColor, greenMaterial);
  }

  private void UpdateForward()
  {
    Vector3 start = transform.position;
    Vector3 end = transform.position + (transform.right * 3f);
    LineContainer.Instance.UpdateLine(forwardLineTag, start, end);
  }
}
