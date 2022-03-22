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

  [SerializeField]
  private float maxSpeed = 10f;

  private PID angleController;

  [SerializeField]
  [Range(-10, 10)]
  private float angleControllerKp, angleControllerKi, angleControllerKd;

  private PID angularVelocityController;
  [SerializeField]
  [Range(-10, 10)]
  private float angularVelocityControllerKp, angularVelocityControllerKi, angularVelocityControllerKd;


  [SerializeField]
  private Material white;

  private bool isThrusting;



  // public float rightDotToTarget;
  // public float toTargetDotRight;
  public float alpha;
  private Vector2 previousLos;
  private Vector2 los;
  private Vector2 losDelta;
  private Vector2 desiredRotation;


  private void Awake()
  {
    angularVelocityController = new PID(angularVelocityControllerKp, angularVelocityControllerKi, angularVelocityControllerKd);
    angleController = new PID(angleControllerKp, angleControllerKi, angleControllerKd);
    rb2d = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    InitializeLines();
  }

  private void Update()
  {
    HandleLineUpdates();

    UpdatePIDTerms();
  }

  private Vector2 RadianToVector2(float radian)
  {
    return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
  }

  private void InitializeLines()
  {
    AddDebugLine("Forward", new Color(255, 0, 0));
    AddDebugLine("Velocity", new Color(0, 255, 68));
    AddDebugLine("Target", Color.white);
    AddDebugLine("Lift", Color.yellow);
  }

  private void HandleLineUpdates()
  {
    UpdateDebugLine("Forward", transform.position, transform.position + (transform.right * 3f));
    UpdateDebugLine("Velocity", transform.position, transform.position + (Vector3)rb2d.velocity);
    UpdateDebugLine("Target", transform.position, targetTransform.position);
  }
  private void UpdatePIDTerms()
  {
    angularVelocityController.Kp = angularVelocityControllerKp;
    angularVelocityController.Ki = angularVelocityControllerKi;
    angularVelocityController.Kd = angularVelocityControllerKd;

    angleController.Kp = angleControllerKp;
    angleController.Ki = angleControllerKi;
    angleController.Kd = angleControllerKd;
  }

  private void FixedUpdate()
  {
    HandleTorque(Time.fixedDeltaTime);
    HandleAoALift(Time.fixedDeltaTime);
    HandleThrust(Time.fixedDeltaTime);

    //// clamp the velocity magnitude
    float speed = rb2d.velocity.magnitude;
    if (speed > maxSpeed)
    {
      rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
    }
  }

  private void HandleTorque(float deltaTime)
  {
    // get the rotationController output (rotation is just the rotation about z)
    float losToTarget = AngleMath.VectorAngle(targetTransform.position - transform.position);
    float angleError = Mathf.DeltaAngle(rb2d.rotation, losToTarget);
    float torqueCorrectionForAngle = angleController.GetOutput(angleError, deltaTime);

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

  private void HandleAoALift(float deltaTime)
  {
    Vector2 liftForce = transform.up * CalcLiftMagnitude(CalcAlpha());
    rb2d.AddForce(liftForce);
  }
  private void HandleThrust(float deltaTime)
  {
    rb2d.AddForce(transform.right * 2f);
  }

  private void Thrust()
  {
    rb2d.AddForce(transform.right * 3f, ForceMode2D.Force);
  }

  private float CalcAlpha()
  {
    if (Vector2.Dot(transform.right, rb2d.velocity) > 0f)
    {
      return Vector2.SignedAngle(rb2d.velocity, transform.right);
    }
    else
    {
      return Vector2.SignedAngle(transform.right, Vector2.zero - rb2d.velocity);
    }
  }

  private float CalcLiftMagnitude(float alpha)
  {
    float AreaFactor = 1f;
    float liftForce = CoefficientOfLift(alpha) * AreaFactor * (rb2d.velocity.magnitude * rb2d.velocity.magnitude) / 2;
    return liftForce;
  }

  private float CoefficientOfLift(float alpha)
  {
    return alpha >= -20f && alpha <= 20f ? (alpha / 20) : alpha < 0 ? -0.5f : 0.5f;
  }

  private void AddDebugLine(string name, Color color)
  {
    Material newMaterial = new Material(white);
    newMaterial.color = color;

    LineContainer.Instance.AddLine(name, Vector3.zero, Vector3.zero, 0.1f, newMaterial);
  }

  private void UpdateDebugLine(string name, Vector3 start, Vector3 end)
  {
    LineContainer.Instance.UpdateLine(name, start, end);
  }
}
