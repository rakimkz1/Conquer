using System;
using TMPro;
using UnityEngine;

namespace BattleField
{
    public class ExtractorHealthHandler
    {
        public float maxHealth;
        private float _health;
        private float _repairmentAmount;
        private AttackableCollection _targetCollection;
        private Extractor _extractor;
        public bool isWorking;
        public event Action OnDead;
        public event Action OnRepaired;
        public event Action<float> OnDamage;
        public ExtractorHealthHandler(Extractor extractor,float maxHealth, float repairmentAmount, AttackableCollection targetCollection)
        {
            this.maxHealth = maxHealth;
            _health = maxHealth;
            _repairmentAmount = repairmentAmount;
            _targetCollection = targetCollection;
            _extractor = extractor;
        }

        public void TakeDamage(float damage)
        {
            _health = Mathf.Clamp(_health - damage, 0, maxHealth);
            OnDamage?.Invoke(_health);
            if (_health <= 0)
            {
                ExtractorDestroyed();
            }
        }
        private void ExtractorDestroyed()
        {
            isWorking = false;
            _targetCollection.RemovePlayerUnit(_extractor);
            OnDead?.Invoke();
        }
        public void Repair()
        {
            if (isWorking)
                return;
            _health = Mathf.Clamp(_health + _repairmentAmount, 0f, maxHealth);
            if(_health == maxHealth)
            {
                isWorking = true;
                OnRepaired?.Invoke();
            }
        }
    }
}