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

        public TargetRangeAttack(bool isEnemy, float Damage, float MissileSpeed)
        {
            this.isEnemy = isEnemy;
            this.Damage = Damage;
            this.MissileSpeed = MissileSpeed;
        }
        public void InitAttack(IAttackTarget attackTarget, Vector3 monsterPosition)
        {
            _attackTarget = attackTarget;
            _rangerPosition = monsterPosition;
        }

        public void InitAttack(Vector3 targetArea, Vector3 monsterPosition) { }
        
        public void Attack()
        {
            Debug.Log("Shoot");
            ShootAttack();
        }

        private async UniTask ShootAttack()
        {
            float flyingTime = FindFlyTime();
            await UniTask.Delay((int)(flyingTime * 1000f));
            Debug.Log("Hit");
            _attackTarget.TakeDamage(Damage);
        }

        private float FindFlyTime()
        {
            return Vector3.Distance(_rangerPosition, _attackTarget.targetPosition) / MissileSpeed;
        }

    }
}
