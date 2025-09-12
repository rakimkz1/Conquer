using System;
using TMPro;
using UnityEngine;

namespace BattleField
{
    public class ExtractorHealthHandler
    {
        private float _maxHealth;
        private float _health;
        private float _repairmentAmount;
        public bool isWorking;
        public event Action OnDead;
        public event Action OnRepaired;

        public ExtractorHealthHandler(float maxHealth, float repairmentAmount)
        {
            _maxHealth = maxHealth;
            _health = maxHealth;
            _repairmentAmount = repairmentAmount;
        }

        public void TakeDamage(float damage)
        {
            _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
            if(_health <= 0)
            {
                OnDead?.Invoke();
            }
        }

        public void Repair()
        {
            if (isWorking)
                return;
            _health = Mathf.Clamp(_health + _repairmentAmount, 0f, _maxHealth);
            if(_health == _maxHealth)
            {
                isWorking = true;
                OnRepaired?.Invoke();
            }
        }
    }
}