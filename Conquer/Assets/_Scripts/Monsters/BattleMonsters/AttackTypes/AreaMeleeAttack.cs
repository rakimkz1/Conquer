using BattleField;
using System.Collections.Generic;
using UnityEngine;

namespace Monsters
{
    public class AreaMeleeAttack : IMonsterAttackType
    {
        public bool isEnemy { get; set; }
        public float Damage { get; set; }
        public float _attackRadius;
        private AttackableUnitsOnSceneCollection _targetCollection;
        private Vector3 _monsterPosition;
        private Vector3 _targetArea;

        public AreaMeleeAttack(bool isEnemy, float damage, float attackRadius, AttackableUnitsOnSceneCollection targetCollection)
        {
            this.isEnemy = isEnemy;
            Damage = damage;
            _attackRadius = attackRadius;
            _targetCollection = targetCollection;
        }
        public void InitAttack(IAttackTarget attackTarget, Vector3 monsterPosition) { }
        public void InitAttack(Vector3 targetArea, Vector3 monsterPosition)
        {
            _monsterPosition = monsterPosition;
            _targetArea = targetArea;
        }

        public void Attack()
        {
            List<IAttackTarget> hitTargets = FindHitTargets();

            foreach (IAttackTarget target in hitTargets)
                target.TakeDamage(Damage);
        }

        private List<IAttackTarget> FindHitTargets()
        {
            List<IAttackTarget> answer = new List<IAttackTarget>();
            if (isEnemy)
            {
                for(int i = 0; i < _targetCollection.playerUnits.Count; i++)
                {
                    float distance = Vector3.Distance(_monsterPosition, _targetCollection.playerUnits[i].targetPosition);
                    if (distance < _attackRadius) 
                        answer.Add(_targetCollection.playerUnits[i]);
                }
            }
            else
            {
                for (int i = 0; i < _targetCollection.enemyUnits.Count; i++)
                {
                    float distance = Vector3.Distance(_monsterPosition, _targetCollection.enemyUnits[i].targetPosition);
                    if (distance < _attackRadius)
                        answer.Add(_targetCollection.enemyUnits[i]);
                }
            }
            return answer;
        }
    }
}
