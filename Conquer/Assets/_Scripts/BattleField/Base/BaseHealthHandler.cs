using System;
using UnityEngine;

namespace BattleField
{
    public class BaseHealthHandler
    {
        public float maxHealth;
        public float health;

        public event Action OnDestroy;
        public event Action<float> OnDamage;
        public BaseHealthHandler(float maxHealth, float health)
        {
            this.maxHealth = maxHealth;
            this.health = health;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Clamp(health - damage, 0f, maxHealth);
            OnDamage?.Invoke(damage);
            if(health <= 0f)
            {
                OnDestroy?.Invoke();
            }
        }
    }
}