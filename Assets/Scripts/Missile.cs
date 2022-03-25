using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
  [SerializeField] private Rigidbody2D tgt_rb2d;
  [SerializeField] private Transform tgt_transform;
  private Vector2 previousTargetPosition;

  private Rigidbody2D rb2d;

  [SerializeField]
  private float maxAngularVelocity = 60f;

  [SerializeField]
  private float torqueFactor = 1f;

  [SerializeField]
  private float maxSpeed = 10f;
  [SerializeField]
  private float maxThrustForce = 5f;

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

  private ProNavGuidance proNavGuidance;

  private void Awake()
  {
    angularVelocityController = new PID(angularVelocityControllerKp, angularVelocityControllerKi, angularVelocityControllerKd);
    angleController = new PID(angleControllerKp, angleControllerKi, angleControllerKd);
    rb2d = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    proNavGuidance = new ProNavGuidance(rb2d, tgt_rb2d);
    rb2d.AddForce(transform.right * 1f, ForceMode2D.Impulse); // initial kick
  }
  private void Update()
  {
    UpdatePIDTerms();
  }

  private Vector2 RadianToVector2(float radian)
  {
    return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
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

  private float goalNavigationInterval = 0.1f;
  private float navigationInterval = 0f;
  private Vector3 desiredRotation;

  private void FixedUpdate()
  {
    DoProNavAngularVelocity(Time.fixedDeltaTime);
    HandleThrust(Time.fixedDeltaTime);
    HandleAoALift(Time.fixedDeltaTime);

    // clamp the velocity magnitude
    float speed = rb2d.velocity.magnitude;
    if (speed > maxSpeed)
    {
      rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
    }
  }

  private Vector2 msl_previous;
  private Vector2 tgt_previous;
  public float currentAngularVelocity;
  public float losDelta;
  public float losDeltaRate;
  public float forceCommanded;
  public float maxForce;
  public string leftOrRight;

  private void DoProNavAngularVelocity(float deltaTime)
  {
    Vector2 los = (Vector2)tgt_transform.position - rb2d.position;
    Vector2 los_previous = tgt_previous - msl_previous;

    losDelta = Vector2.SignedAngle(los_previous, los);
    losDeltaRate = losDelta / deltaTime;

    // is thing left/right
    if (Vector2.SignedAngle(transform.right, los) > 0)
    {
      leftOrRight = "left"; // anti-clockwise (+ rotation)
    }
    else
    {
      leftOrRight = "right"; // clockwise (- rotation)
    }

    float torqueCorrectionForAnglularVelocity = angularVelocityController.GetOutput(losDelta, deltaTime);

    maxForce = maxAngularVelocity * Mathf.Deg2Rad * rb2d.inertia;
    // float force = Mathf.Clamp(torqueFactor * torqueCorrectionForAnglularVelocity, -maxForce, maxForce);
    // rb2d.AddTorque(force);
    rb2d.AddTorque(torqueCorrectionForAnglularVelocity);

    tgt_previous = tgt_transform.position;
    msl_previous = rb2d.position;

    currentAngularVelocity = rb2d.angularVelocity;
    forceCommanded = torqueCorrectionForAnglularVelocity;
  }

  private void HandleTorque(Vector3 desiredRotation, float deltaTime)
  {
    Debug.DrawRay(rb2d.position, AngleMath.DegreeToVector2(desiredRotation.z), Color.yellow);
    Debug.DrawRay(rb2d.position, AngleMath.DegreeToVector2(rb2d.rotation), Color.cyan);

    float angleError = desiredRotation.z - rb2d.rotation;
    float torqueCorrectionForAngle = angleController.GetOutput(angleError, deltaTime);



    // get the angularVelocity controller output
    // angular velocity controller wants to drive the angular velocity to 0...i.e. the Set Point is 0, so the error is always -angularVelocity
    float angularVelocityError = 0f - rb2d.angularVelocity;
    float torqueCorrectionForAnglularVelocity = angularVelocityController.GetOutput(angularVelocityError, deltaTime);

    float maxForce = maxAngularVelocity * Mathf.Deg2Rad * rb2d.inertia;
    float force = Mathf.Clamp(torqueFactor * (torqueCorrectionForAngle + torqueCorrectionForAnglularVelocity), -maxForce, maxForce);
    rb2d.AddTorque(force);
  }

  private void HandleTorqueOriginal(float deltaTime)
  {
    // get the rotationController output (rotation is just the rotation about z)
    float losToTarget = AngleMath.VectorAngle(tgt_transform.position - transform.position);
    float angleError = Mathf.DeltaAngle(rb2d.rotation, losToTarget);
    float torqueCorrectionForAngle = angleController.GetOutput(angleError, deltaTime);

    // los rate = (current - previous) / deltaTime. want this = 0;
    float currentLos = AngleMath.VectorAngle(tgt_transform.position - transform.position);

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
    rb2d.AddForce(transform.right * 3f);
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
}
