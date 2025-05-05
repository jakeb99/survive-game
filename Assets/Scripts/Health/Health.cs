using System;
using UnityEngine;

public class Health : MonoBehaviour
{
   [field: SerializeField] public float maxHealth { get; private set; }
   [field: SerializeField] public float currentHealth { get; private set; }

    public Action OnDeath;
    public Action OnDecrementHealth;
    public Action OnIncrementHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void DecrementHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDecrementHealth?.Invoke();
            OnDeath?.Invoke();
        }
        OnDecrementHealth?.Invoke();
    }

    public void IncrementHealth(float amount)
    {
        if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
            OnIncrementHealth?.Invoke();
        } else
        {
            currentHealth += amount;
            OnIncrementHealth?.Invoke();
        }
    }

    public void ResetToMaxHealth()
    {
        currentHealth = maxHealth;
        OnIncrementHealth?.Invoke();
    }

    public void SetCurrentHealth(float x)
    {
        currentHealth = x;
    }
}
