using UnityEngine;

namespace Monsters
{
    public class TargetMeleeAttack : IMonsterAttackType
    {

        public bool isEnemy { get; set; }
        public float Damage { get; set; }

        private IAttackTarget _attackTarget;
        private Vector3 _monsterPosition;
        private MonsterIdelData _monsterData;
        public TargetMeleeAttack(bool isEnemy, float Damage)
        {
            this.isEnemy = isEnemy;
            this.Damage = Damage;
        }
        public void InitAttack(IAttackTarget attackTarget, Vector3 monsterPosition, MonsterIdelData data)
        {
            _attackTarget = attackTarget;
            _monsterPosition = monsterPosition;
            _monsterData = data;
        }

        public void InitAttack(Vector3 targetArea, Vector3 monsterPosition, MonsterIdelData data) { }

        public void Attack()
        {
            _attackTarget.TakeDamage(Damage);
        }
    }
}
