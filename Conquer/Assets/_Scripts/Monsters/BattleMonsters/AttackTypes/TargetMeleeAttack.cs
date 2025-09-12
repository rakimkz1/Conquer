using UnityEngine;

namespace Monsters
{
    public class TargetMeleeAttack : IMonsterAttackType
    {

        public bool isEnemy { get; set; }
        public float Damage { get; set; }

        private IAttackTarget _attackTarget;
        private Vector3 _monsterPosition;
        public TargetMeleeAttack(bool isEnemy, float Damage)
        {
            this.isEnemy = isEnemy;
            this.Damage = Damage;
        }
        public void InitAttack(IAttackTarget attackTarget, Vector3 monsterPosition)
        {
            _attackTarget = attackTarget;
            _monsterPosition = monsterPosition;
        }

        public void InitAttack(Vector3 targetArea, Vector3 monsterPosition) { }

        public void Attack()
        {
            _attackTarget.TakeDamage(Damage);
        }

    }
}
