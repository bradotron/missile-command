using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vector2Reference
{
  public Vector2Variable Variable;
  public bool IsConstant = true;
  public Vector2 ConstantValue;

  public Vector2Reference() { }

  public Vector2Reference(Vector2 value)
  {
    IsConstant = true;
    ConstantValue = value;
  }

  public Vector2 Value
  {
    get
    {
      return IsConstant ? ConstantValue : Variable.Value;
    }
  }

  public static implicit operator Vector2(Vector2Reference reference)
  {
    return reference.Value;
  }
}
