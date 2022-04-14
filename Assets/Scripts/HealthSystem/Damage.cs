using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public enum DamageType
{
  Explosive,
  Kinetic,
  Electric
}

[Serializable]
public struct Damage
{
  public DamageType damageType;
  public float amount;
}
