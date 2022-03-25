using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AngleMath
{
  public enum AngleUnits
  {
    Rad,
    Deg
  }
  public static float VectorAngle(Vector2 vector2, AngleUnits units = AngleUnits.Deg)
  {
    switch (units)
    {
      case AngleUnits.Deg:
        return Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg;

      case AngleUnits.Rad:
        return Mathf.Atan2(vector2.y, vector2.x);

      default:
        return -1f;
    }
  }
  public static Vector2 RadianToVector2(float radian)
  {
    return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
  }

  public static Vector2 DegreeToVector2(float degree)
  {
    return RadianToVector2(degree * Mathf.Deg2Rad);
  }

  /// <summary>
  /// Use AddTorque() to face a specific angle. For 2D physiscs. Should be called in every FixedUpdate() frame.
  /// </summary>
  /// <param name="currentVec"> vector representing the direction we are currently pointing at. (transform.right) </param>
  /// <param name="targetVec"> vector representing the direction we want to point at. </param>
  /// <param name="rb"> Rigidbody to affect. </param>
  /// <param name="maxTorque"> Max torque to apply. </param>
  /// <param name="torqueDampFactor"> Damping factor to avoid undershooting. </param>
  /// <param name="offsetForgive"> Stop applying force when the angles are within this threshold (default 0). </param>
  public static void TorqueTo(Vector3 currentVec, Vector3 targetVec, Rigidbody2D rb, float maxTorque, float torqueDampFactor, float offsetForgive = 0)
  {
    float targetAngle = FindAngle(targetVec);
    float currentAngle = FindAngle(currentVec);
    float angleDifference = AngleDifference(targetAngle, currentAngle);
    if (Mathf.Abs(angleDifference) < offsetForgive) return;

    float torqueToApply = maxTorque * angleDifference / 180f;
    torqueToApply -= rb.angularVelocity * torqueDampFactor;
    rb.AddTorque(torqueToApply, ForceMode2D.Force);
  }
 
  /// <summary>
  /// Returns the angle (in degrees) in which the vector is pointing.
  /// </summary>
  /// <returns>0-360 angle </returns>
  public static float FindAngle(Vector2 vec)
  {
    return FindAngle(vec.x, vec.y);
  }
  public static float FindAngle(float x, float y)
  {
    float value = (float)((System.Math.Atan2(y, x) / System.Math.PI) * 180);
    if (value < 0) value += 360f;
    return value;
  }
  public static float AngleDifference(float a, float b)
  {
    return ((((a - b) % 360f) + 540f) % 360f) - 180f;
  }
}