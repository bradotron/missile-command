using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
  private int currentHealth;
  [SerializeField] private int maxHealth;
  private Dictionary<DamageType, float> damageVulnerabilities;

  public event EventHandler OnDied;

  private void Awake()
  {
    currentHealth = maxHealth;
  }

  public void SetDamageVulnerabilities(Dictionary<DamageType, float> vulnerabilities)
  {
    damageVulnerabilities = vulnerabilities;
  }

  public void Damage(Damage damage)
  {
    if (damageVulnerabilities.ContainsKey(damage.damageType))
    {
      currentHealth -= Mathf.RoundToInt(damage.amount * damageVulnerabilities[damage.damageType]);
      currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

      if (IsDead())
      {
        OnDied?.Invoke(this, EventArgs.Empty);
      }
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
