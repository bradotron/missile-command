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
}