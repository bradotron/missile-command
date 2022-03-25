using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProNavGuidance
{
  private Rigidbody2D msl_rb2d;
  private Rigidbody2D tgt_rb2d;
  private float N = 5f;
  private float Nt = 9.8f;

  private Vector3 msl_pos;
  private Vector3 msl_pos_previous;
  private Vector3 tgt_pos;
  private Vector3 tgt_pos_previous;
  private Vector3 Rtm;
  private Vector3 Rtm_previous;
  private float los_delta;
  private float los_delta_previous;
  private Vector3 desiredRotation;


  public ProNavGuidance(Rigidbody2D msl, Rigidbody2D tgt, float N = 5f, float Nt = 9.8f)
  {
    msl_rb2d = msl;
    tgt_rb2d = tgt;
    this.N = N;
    this.Nt = Nt;

    msl_pos = msl_rb2d.position;
    msl_pos_previous = msl_rb2d.position;
    tgt_pos = tgt_rb2d.position;
    tgt_pos_previous = tgt_rb2d.position;
    desiredRotation = Vector3.zero;
  }

  public Vector3 RotationCommand(float deltaTime)
  {
    msl_pos = msl_rb2d.position;
    tgt_pos = tgt_rb2d.position;
    Rtm = tgt_pos - msl_pos;

    Vector3 predictedIntercept = CalculatePredictedIntercept(deltaTime);
    // float rotationMagnitude = Vector2.S(msl_rb2d.transform.right, predictedIntercept);
    float rotationDirection = Vector2.SignedAngle(msl_rb2d.transform.right, predictedIntercept);
    // rotationDirection = rotationDirection / Mathf.Abs(rotationDirection);
    desiredRotation.z = rotationDirection;

    msl_pos_previous = msl_pos;
    tgt_pos_previous = tgt_pos;
    Rtm_previous = Rtm;

    return desiredRotation;
  }

  private float CalculateLosDelta(float deltaTime)
  {
    Debug.DrawLine(msl_pos, msl_pos + msl_rb2d.transform.right * 5f, Color.white, deltaTime);
    Debug.DrawLine(msl_pos, msl_pos + msl_rb2d.transform.up * 5f, Color.gray, deltaTime);

    Debug.DrawLine(msl_pos, msl_pos + Rtm, Color.red, deltaTime);
    Debug.DrawLine(msl_pos, msl_pos + Rtm_previous, Color.magenta, deltaTime);

    float deltaLos = Vector2.SignedAngle(Rtm_previous, Rtm);
    Debug.Log(deltaLos);
    if (deltaLos > 0)
    {
      Debug.Log("rate of change of los is anti-clockwise");
    }
    else
    {
      Debug.Log("rate of change of los is clockwise");
    }

    // // is thing left/right
    // if (Vector2.SignedAngle(msl_rb2d.transform.right, Rtm) > 0)
    // {
    //   Debug.Log("anti-clockwise from right");
    // }
    // else
    // {
    //   Debug.Log("clockwise from right");
    // }

    // // is thing front/behind
    // if (Vector2.SignedAngle(msl_rb2d.transform.up, Rtm) > 0)
    // {
    //   Debug.Log("anti-clockwise from up");
    // }
    // else
    // {
    //   Debug.Log("clockwise from up");
    // }


    return deltaLos;
  }


  private Vector3 CalculatePredictedIntercept(float deltaTime)
  {
    Debug.DrawLine(tgt_pos, msl_pos, Color.black, deltaTime);

    Vector3 V_tgt = tgt_rb2d.velocity;
    Vector3 V_msl = msl_rb2d.velocity;

    Vector3 test = (tgt_pos - tgt_pos_previous) / deltaTime;
    Debug.Log($"rb2d_vel: {V_tgt} | calcd: {test}");

    if (V_tgt.magnitude >= -0.1f && V_tgt.magnitude <= 0.1f)
    {
      return tgt_rb2d.position;
    }

    float timeToDirectIntercept = Rtm.magnitude / V_msl.magnitude;

    Vector3 tgt_pos_predicted = tgt_pos + V_tgt * timeToDirectIntercept;

    Debug.DrawLine(tgt_pos, tgt_pos_predicted, Color.red, deltaTime);
    Debug.DrawLine(msl_pos, tgt_pos_predicted, Color.green, deltaTime);

    return tgt_pos_predicted;
  }

}
