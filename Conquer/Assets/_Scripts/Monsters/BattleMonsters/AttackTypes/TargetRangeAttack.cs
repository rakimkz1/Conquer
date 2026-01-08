using BattleField;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Monsters
{
    public class TargetRangeAttack : IMonsterAttackType
    {
        public bool isEnemy { get; set; }
        public float Damage { get; set; }

        public float MissileSpeed;

        private IAttackTarget _attackTarget;
        private Vector3 _rangerPosition;
        private MonsterIdelData _monsterData;
        private ProjectileViewManager _projectileManager;

        public TargetRangeAttack(bool isEnemy, float Damage, float MissileSpeed, ProjectileViewManager projectileViewManager)
        {
            this.isEnemy = isEnemy;
            this.Damage = Damage;
            this.MissileSpeed = MissileSpeed;
            _projectileManager = projectileViewManager;
        }
        public void InitAttack(IAttackTarget attackTarget, Vector3 monsterPosition, MonsterIdelData data)
        {
            _attackTarget = attackTarget;
            _rangerPosition = monsterPosition;
            _monsterData = data;
        }

        public void InitAttack(Vector3 targetArea, Vector3 monsterPosition, MonsterIdelData data) { }
        
        public void Attack()
        {
            ShootAttack();
        }

        private async UniTask ShootAttack()
        {
            float flyingTime = FindFlyTime();
            if (_monsterData.monsterType == MonsterType.Rangers)
                _projectileManager.ShootTargetProjectile(_rangerPosition, _attackTarget.targetPosition, flyingTime, _monsterData, isEnemy);
            else
                _projectileManager.ShootSiegeProjectile(_rangerPosition, _attackTarget.targetPosition, flyingTime, _monsterData, isEnemy);
            await UniTask.Delay((int)(flyingTime * 1000f));
            if (_attackTarget != null)
                _attackTarget.TakeDamage(Damage);
        }

        private float FindFlyTime()
        {
            return Vector3.Distance(_rangerPosition, _attackTarget.targetPosition.position) / MissileSpeed;
        }
    }
}
