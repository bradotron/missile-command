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
  private PID losRateOfChangeController;
  [SerializeField]
  [Range(-100, 100)]
  private float losRateOfChangeControllerKp, losRateOfChangeControllerKi, losRateOfChangeControllerKd;




  [SerializeField]
  private Material white;

  private bool isThrusting;



  // public float rightDotToTarget;
  // public float toTargetDotRight;
  public float alpha;
  private Vector3 previousLos;
  private Vector3 los;
  private Vector3 losDelta;
  private Vector3 desiredRotation;


  private void Awake()
  {
    losRateOfChangeController = new PID(losRateOfChangeControllerKp, losRateOfChangeControllerKi, losRateOfChangeControllerKd);
    // angularVelocityController = new PID(angularVelocityControllerKp, angularVelocityControllerKi, angularVelocityControllerKd);
    // angleController = new PID(angleControllerKp, angleControllerKi, angleControllerKd);
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
    AddDebugLine("Forward", Color.blue);
    AddDebugLine("Velocity", Color.green);
    //AddDebugLine("Target", Color.white);

    AddDebugLine("los", Color.white);
    AddDebugLine("previousLos", Color.grey);
    AddDebugLine("deltaLos", Color.yellow);
    AddDebugLine("goalAngle", Color.white);
    AddDebugLine("desiredRotation", Color.magenta);
  }

  private void HandleLineUpdates()
  {
    UpdateDebugLine("Forward", transform.position, transform.position + (transform.right * 3f));
    UpdateDebugLine("Velocity", transform.position, transform.position + (Vector3)rb2d.velocity);
    //UpdateDebugLine("Target", transform.position, targetTransform.position);
  }
  private void UpdatePIDTerms()
  {
    // angularVelocityController.Kp = angularVelocityControllerKp;
    // angularVelocityController.Ki = angularVelocityControllerKi;
    // angularVelocityController.Kd = angularVelocityControllerKd;

    // angleController.Kp = angleControllerKp;
    // angleController.Ki = angleControllerKi;
    // angleController.Kd = angleControllerKd;

    losRateOfChangeController.Kp = losRateOfChangeControllerKp;
    losRateOfChangeController.Ki = losRateOfChangeControllerKi;
    losRateOfChangeController.Kd = losRateOfChangeControllerKd;
  }

  private void FixedUpdate()
  {
    HandleTorqueProNav(Time.fixedDeltaTime);
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

  [SerializeField]
  [Tooltip("Proportionallity Constant")]
  [Range(0f, 10f)]
  private float N = 5f;
  private void HandleTorqueProNav(float deltaTime)
  {
    previousLos = los;
    los = targetTransform.position - transform.position;
    float angleDelta = Vector2.SignedAngle(los, previousLos);

    float losChangeRate = angleDelta * deltaTime;
    float losChangeRateError = 0f - losChangeRate;

    float losChangeRateCorrection = losRateOfChangeController.GetOutput(losChangeRateError, deltaTime);

    float maxForce = maxAngularVelocity * Mathf.Deg2Rad * rb2d.inertia;
    float force = Mathf.Clamp(torqueFactor * losChangeRateCorrection * N, -maxForce, maxForce);
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
