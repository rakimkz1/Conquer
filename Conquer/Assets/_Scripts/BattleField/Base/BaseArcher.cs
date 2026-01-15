using Cysharp.Threading.Tasks;
using Monsters;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace BattleField
{
    public class BaseArcher
    {
        private float _damage;
        private float _attackColdown;
        private float _attackDistance;
        private float _projectileSpeed;
        private Vector3 _archerPosition;
        private bool _isEnemy;
        private AttackableCollection _attackableCollection;
        private ProjectileViewManager _projectileViewManager;
        private CancellationTokenSource _cancelSource;
        public BaseArcher(float damage, float attackColdown, float attackDistance, float projectileSpeed, Vector3 archerPosition,  bool isEnemy, AttackableCollection attackableCollection, ProjectileViewManager projectileViewManager)
        {
            _damage = damage;
            _attackColdown = attackColdown;
            _attackDistance = attackDistance;
            _archerPosition = archerPosition;
            _projectileSpeed = projectileSpeed;
            _isEnemy = isEnemy;
            _attackableCollection = attackableCollection;
            _projectileViewManager = projectileViewManager;
        }

        public async UniTask StartAttack()
        {
            _cancelSource = new CancellationTokenSource();

            while (!_cancelSource.IsCancellationRequested)
            {
                IAttackTarget attackTarget = FoundTarget();
                if (attackTarget != null)
                    Attack(attackTarget);

                await UniTask.Delay((int)(_attackColdown * 1000f),cancellationToken: _cancelSource.Token);
            }
        }

        public void StopAttack()
        {
            _cancelSource?.Cancel();
        }

        public IAttackTarget FoundTarget()
        {
            List<IAttackTarget> targets;
            List<IAttackTarget> foundedTarget = new List<IAttackTarget>();
            if (_isEnemy)
                targets = _attackableCollection.playerUnits;
            else
                targets = _attackableCollection.enemyUnits;

            for (int i = 0; i < targets.Count; i++)
            {
                if (GetDistance(targets[i]) < _attackDistance)
                    foundedTarget.Add(targets[i]);
            }
            if (foundedTarget.Count == 0)
                return null;
            int randomTarget = Random.Range(0, foundedTarget.Count);
            return foundedTarget[randomTarget];
        }

        public async UniTask Attack(IAttackTarget attackTarget)
        {
            float flyingTime = GetDistance(attackTarget) / _projectileSpeed;
            _projectileViewManager.ShootBaseArcherProjectile(_archerPosition, attackTarget.targetPosition, flyingTime, _isEnemy);
            await UniTask.Delay((int)(flyingTime * 1000f));
            if (attackTarget != null)
                attackTarget.TakeDamage(_damage);
        }
        private float GetDistance(IAttackTarget attackTarget)
        {
            return Vector3.Distance(_archerPosition, attackTarget.targetPosition.position);
        }
    }
}
