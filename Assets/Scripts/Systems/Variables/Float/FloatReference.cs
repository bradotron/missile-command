[System.Serializable]
public class FloatReference
{
  public FloatVariable Variable;
  public bool IsConstant = true;
  public float ConstantValue;

  public FloatReference() { }

  public FloatReference(float value)
  {
    IsConstant = true;
    ConstantValue = value;
  }

  public float Value
  {
    get
    {
      return IsConstant ? ConstantValue : Variable.Value;
    }
  }

  public static implicit operator float(FloatReference reference)
  {
    return reference.Value;
  }
}
