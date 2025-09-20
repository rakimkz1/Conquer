using Cysharp.Threading.Tasks;
using Monsters;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace BattleField
{
    public class EnemyExtractor : MonoBehaviour, IAttackTarget
    {
        public float periodTime;
        public float manaPerPeriod;
        public float maxHealth;
        public float health;
        public bool isBroken;
        private EnemyManaHandler _enemyManaHandler;
        private CancellationTokenSource _cancellation;
        private BattleStarter _battleStarter;
        public float attackPriority { get; set; }
        public Vector3 targetPosition { get; set; }
        public float powerScale { get; set; }
        public bool isDead { get; set; }

        public event Action<IAttackTarget> OnDead;
        public event Action<IAttackTarget> OnExitTargetCollection;

        [Inject]
        public void Construct(EnemyManaHandler enemyManaHandler, BattleStarter battleStarter)
        {
            _enemyManaHandler = enemyManaHandler;
            _battleStarter = battleStarter;
            targetPosition = transform.position;
            _battleStarter.OnBattleStart += StartManaProducing;
        }

        public void SetProperties(float periodTime, float manaPerPeriod, float maxHealth, float extractorAttackPriotity)
        {
            this.periodTime = periodTime;
            this.manaPerPeriod = manaPerPeriod;
            this.maxHealth = maxHealth;
            attackPriority = extractorAttackPriotity;
        }

        public async void StartManaProducing()
        {
            _cancellation = new CancellationTokenSource();

            while(_cancellation.IsCancellationRequested == false)
            {
                try
                {
                    await UniTask.Delay((int)(periodTime * 1000f), cancellationToken: _cancellation.Token);
                }
                catch { return; }
                _enemyManaHandler.AddMana(manaPerPeriod);
            }
        }

        public void StopManaProducing()
        {
            _cancellation.Cancel();
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0f)
                Dead();
        }

        private void Dead()
        {
            OnDead?.Invoke(this);
            isBroken = true;
            StopManaProducing();
        }
    }
}