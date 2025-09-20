using BattleField;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Monsters
{
    public class AttackTargetFinder
    {
        private AttackableUnitsOnSceneCollection _targetCollection;
        private BattleMonster _monster;
        public IAttackTarget currentAttackTarget;

        public AttackTargetFinder(AttackableUnitsOnSceneCollection targetCollection, BattleMonster monster)
        {
            _targetCollection = targetCollection;
            _monster = monster;
        }

        public void FindAttackTarget()
        {
            List<IAttackTarget> targetList;
            if (!_monster.isEnemyUnit)
                targetList = _targetCollection.enemyUnits;
            else
                targetList = _targetCollection.playerUnits;

            float minDistance = 10000000f;
            IAttackTarget suitableTarget = targetList[0];
            for(int i = 0; i < targetList.Count; i++)
            {
                float dis = Vector3.Distance(_monster.targetPosition, targetList[i].targetPosition) * targetList[i].attackPriority;
                if(dis < minDistance)
                {
                    minDistance = dis;
                    suitableTarget = targetList[i];
                }
            }
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

        public void OnEnemyIsLost(IAttackTarget monster)
        {
            Debug.Log("Find new Target");
            FindAttackTarget();
        }
    }
}