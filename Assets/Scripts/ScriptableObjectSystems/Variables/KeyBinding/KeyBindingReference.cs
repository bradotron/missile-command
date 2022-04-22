using UnityEngine;

[System.Serializable]
public class KeyBindingReference
{
  public KeyBindingVariable Variable;
  public bool IsConstant = true;
  public KeyCode ConstantValue;

  public KeyBindingReference() { }

  public KeyBindingReference(KeyCode value)
  {
    IsConstant = true;
    ConstantValue = value;
  }

  public KeyCode Value
  {
    get
    {
      return IsConstant ? ConstantValue : Variable.Value;
    }
  }

  public static implicit operator KeyCode(KeyBindingReference reference)
  {
    return reference.Value;
  }
}
