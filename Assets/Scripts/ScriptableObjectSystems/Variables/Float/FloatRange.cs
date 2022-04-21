using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloatRange
{
  public FloatReference Min;
  public FloatReference Max;

  public float Value
  {
    get
    {
      return UnityEngine.Random.Range(Min, Max);
    }
  }
}
