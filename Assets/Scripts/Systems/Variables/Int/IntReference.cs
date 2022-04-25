[System.Serializable]
public class IntReference
{
  public IntVariable Variable;
  public bool IsConstant = true;
  public int ConstantValue;

  public IntReference() { }

  public IntReference(int value)
  {
    IsConstant = true;
    ConstantValue = value;
  }

  public int Value
  {
    get
    {
      return IsConstant ? ConstantValue : Variable.Value;
    }
  }

  public static implicit operator int(IntReference reference)
  {
    return reference.Value;
  }
}
