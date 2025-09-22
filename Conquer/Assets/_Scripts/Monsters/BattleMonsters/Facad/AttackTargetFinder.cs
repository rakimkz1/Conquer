using BattleField;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Linq;

namespace Monsters
{
    public class AttackTargetFinder
    {
        private AttackableUnitsOnSceneCollection _targetCollection;
        private BattleMonster _monster;
        public IAttackTarget currentAttackTarget;
        private CancellationTokenSource _cancellation;
        public AttackTargetFinder(AttackableUnitsOnSceneCollection targetCollection, BattleMonster monster)
        {
            _targetCollection = targetCollection;
            _monster = monster;
            _monster.OnDead += StopScan;
            TargetLoopScan();
        }
        public void FindAttackTarget()
        {
            List<IAttackTarget> targetList;
            if (!_monster.isEnemyUnit)
                targetList = _targetCollection.enemyUnits;
            else
                targetList = _targetCollection.playerUnits;

            IAttackTarget suitableTarget = targetList.Aggregate((a, b) => 
            Vector3.Distance(_monster.targetPosition, a.targetPosition) < Vector3.Distance(_monster.targetPosition, b.targetPosition)? a : b);
            SetAttackTarget(suitableTarget);
        }
        private void SetAttackTarget(IAttackTarget newAttackTarget)
        {
            if (currentAttackTarget != null)
            {
                currentAttackTarget.OnExitTargetCollection -= OnEnemyIsLost;
                currentAttackTarget.OnDead -= OnEnemyIsLost;
            }
            currentAttackTarget = newAttackTarget;
            currentAttackTarget.OnExitTargetCollection += OnEnemyIsLost;
            currentAttackTarget.OnDead += OnEnemyIsLost;
        }

        private async void TargetLoopScan()
        {
            _cancellation = new CancellationTokenSource();
            while (!_cancellation.IsCancellationRequested)
            {
                FindAttackTarget();
                try
                {
                    await UniTask.Delay(UnityEngine.Random.Range(800, 1200), cancellationToken: _cancellation.Token);
                }
                catch { return; }
            }
        }

        public void StopScan(IAttackTarget target)
        {
            _cancellation?.Cancel();
        }

        public void OnEnemyIsLost(IAttackTarget monster)
        {
            FindAttackTarget();
        }
    }
}