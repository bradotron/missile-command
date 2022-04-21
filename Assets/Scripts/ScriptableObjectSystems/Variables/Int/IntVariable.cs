using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Int")]
public class IntVariable : ScriptableObject
{
  public int Value;
  
  public void SetValue(int value)
  {
    Value = value;
  }
}
