using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Monsters
{
    public class HealthHandler
    {
        public ReactiveProperty<float> unitHealth = new ReactiveProperty<float>();
        public ReactiveProperty<bool> isUnitOutOfBattle = new ReactiveProperty<bool>();
        public float maxHealth;
        public float retreadHealAmount;
        public float retreatHealColdown;

        public event Action OnDead;
        private CancellationTokenSource _cancelToken;
        
        public HealthHandler()
        {
            isUnitOutOfBattle.Subscribe(value =>
            {
                if (value)
                    GoOutOfBattle();
                else
                    EnterToBattle();
            });
        }

        public void GoOutOfBattle()
        {
            HealUnitUntilOutOfBattle();
        }

        private async UniTask HealUnitUntilOutOfBattle()
        {
            _cancelToken = new CancellationTokenSource();
            while (!_cancelToken.IsCancellationRequested)
            {
                try{
                    await UniTask.Delay((int)(retreatHealColdown * 1000f), cancellationToken: _cancelToken.Token);
                    HealUnit(retreadHealAmount);
                }
                catch { }
            }
        }

        public void EnterToBattle()
        {
            _cancelToken?.Cancel();
        }

        public void TakeDamage(float damage)
        {
            unitHealth.Value -= damage;
            if (unitHealth.Value <= 0f)
                OnDead?.Invoke();
        }

        public void HealUnit(float healthAmount)
        {
            unitHealth.Value += healthAmount;
            unitHealth.Value = Mathf.Clamp(unitHealth.Value, 0f, maxHealth);
        }
    }
}