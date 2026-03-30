using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;
    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public event Action<float, float> OnDamage; // current health, damage taken
    public event Action<float, float> OnHeal; // current health, ammount healed
    public event Action<GameObject, GameObject> OnDeath; // self, attacker

    private void Start()
    {
        if (_maxHealth <= 0)
        {
            Debug.LogError("[HealthComponent] Max health has to be greater than 0!");
            return;
        }
        _currentHealth = MaxHealth;

        TakeDamage(10, gameObject);
    }

    public void TakeDamage(float damage, GameObject attacker)
    {
        if (attacker == null)
        {
            Debug.LogError("[HealthComponent] Attacker not set!");
            return;
        }

        _currentHealth = Mathf.Max(_currentHealth - damage, 0);

        OnDamage?.Invoke(CurrentHealth, damage);
        Debug.Log($"[HealthComponent] Taken damage from {attacker.name}");

        if (_currentHealth <= 0)
        {
            OnDeath?.Invoke(gameObject, attacker);
        }
    }

    public void Heal(float ammount)
    {
        _currentHealth = MathF.Min(_currentHealth + ammount, MaxHealth);
        OnHeal?.Invoke(CurrentHealth, ammount);
    }
}