using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Float")]
public class FloatVariable : ScriptableObject
{
  public float Value;

  public void SetValue(float value)
  {
    Value = value;
  }

}

