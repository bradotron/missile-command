using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSO : ScriptableObject
{
  public float startRadius;
  public float endRadius;
  public float duration;
  public float damage; // could replace this with a damage class / SO
}
