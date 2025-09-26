using BattleField;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.AddressableAssets.GUI;
using UnityEngine;

namespace Monsters
{
    public class AreaRangeAttack : IMonsterAttackType
    {
        public bool isEnemy { get; set; }
        public float Damage { get; set; }

        private float _attackRadius;
        private float _missileSpeed;
        private AttackableCollection _targetCollection;
        private Vector3 _targetPosition;
        private Vector3 _rangerPosition;

        public AreaRangeAttack(bool isEnemy, float damage, float attackRadius, float missileSpeed, AttackableCollection targetCollection)
        {
            this.isEnemy = isEnemy;
            Damage = damage;
            _missileSpeed = missileSpeed;
            _targetCollection = targetCollection;
        }

        public void InitAttack(IAttackTarget attackTarget, Vector3 monsterPosition) { }

        public void InitAttack(Vector3 targetArea, Vector3 monsterPosition)
        {
            _targetPosition = targetArea;
            _rangerPosition = monsterPosition;
        }

        public void Attack()
        {
            ShootAttack();
        }

        private async UniTask ShootAttack()
        {
            float flyingTime = FindFlyTime();
            await UniTask.Delay((int)(flyingTime * 1000f));
            List<IAttackTarget> targetList = FindTargetsInArea();
            foreach (var target in targetList)
            {
                target.TakeDamage(Damage);
            }
        }

        private List<IAttackTarget> FindTargetsInArea()
        {
            List<IAttackTarget> answer = new List<IAttackTarget>();
            if (isEnemy)
            {
                for(int i = 0;i < _targetCollection.playerUnits.Count; i++)
                {
                    float distance = Vector3.Distance(_targetPosition, _targetCollection.playerUnits[i].targetPosition);
                    if (distance < _attackRadius)
                        answer.Add(_targetCollection.playerUnits[i]);
                }
            }
            else
            {
                for (int i = 0; i < _targetCollection.enemyUnits.Count; i++)
                {
                    float distance = Vector3.Distance(_targetPosition, _targetCollection.enemyUnits[i].targetPosition);
                    if (distance < _attackRadius)
                        answer.Add(_targetCollection.enemyUnits[i]);
                }
            }
            return answer;
        }
        private float FindFlyTime()
        {
            return Vector3.Distance(_rangerPosition, _targetPosition) / _missileSpeed;
        }
    }
}
