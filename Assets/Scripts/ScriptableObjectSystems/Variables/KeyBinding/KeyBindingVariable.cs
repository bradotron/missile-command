using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Key Binding")]
public class KeyBindingVariable : ScriptableObject
{
  public KeyCode Value;

  public void SetValue(KeyCode value)
  {
    Value = value;
  }
}
