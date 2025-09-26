using System;
using UnityEngine;

namespace BattleField
{
    public class BaseHealthHandler
    {
        private float _maxHealth;
        private float _health;


        public event Action OnDestroy;
        public event Action OnDamage;
        public BaseHealthHandler(float maxHealth, float health)
        {
            _maxHealth = maxHealth;
            _health = health;
        }

        public void TakeDamage(float damage)
        {
            _health = Mathf.Clamp(_health - damage, 0f, _maxHealth);
            OnDamage?.Invoke();
            if(_health <= 0f)
            {
                OnDestroy?.Invoke();
            }
        }
    }
}