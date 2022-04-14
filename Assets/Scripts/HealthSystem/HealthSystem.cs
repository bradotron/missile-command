using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
  private int currentHealth;
  [SerializeField] private int maxHealth;
  [SerializeField] private List<Damage> damageResistances;
  private Dictionary<DamageType, float> damageResistanceDictionary;

  public event EventHandler OnDied;

  private void Awake()
  {
    currentHealth = maxHealth;
    damageResistanceDictionary = new Dictionary<DamageType, float>();
    foreach (Damage damage in damageResistances)
    {
      if (damage.amount <= 0f || damage.amount >= 1f)
      {
        damageResistanceDictionary.Add(damage.damageType, damage.amount);
      }
      else
      {
        Debug.LogError("a damage resistance amount must be between 0 and 1");
      }
    }
  }

  public void Damage(Damage damage)
  {
    if (damageResistanceDictionary.ContainsKey(damage.damageType))
    {
      // take mitigated damage
      currentHealth -= Mathf.RoundToInt(damage.amount * damageResistanceDictionary[damage.damageType]);
    }
    else
    {
      // take full damage
      currentHealth -= Mathf.RoundToInt(damage.amount);
    }

    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    if (IsDead())
    {
      OnDied?.Invoke(this, EventArgs.Empty);
    }
  }

  public void Damages(List<Damage> damages)
  {
    foreach (Damage damage in damages)
    {
      Damage(damage);
    }
  }

  public bool IsDead()
  {
    return currentHealth == 0;
  }
}
