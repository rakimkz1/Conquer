using BattleField;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Monsters
{
    public class AttackTargetFinder
    {
        private AttackableUnitsOnSceneCollection _targetCollection;
        public IAttackTarget currentAttackTarget;

        public AttackTargetFinder(AttackableUnitsOnSceneCollection targetCollection)
        {
            _targetCollection = targetCollection;
        }

        public void FindAttackTarget(bool isFindEnemy, Vector3 pos)
        {
            List<IAttackTarget> targetList;
            if (isFindEnemy)
                targetList = _targetCollection.enemyUnits;
            else
                targetList = _targetCollection.playerUnits;

            float minDistance = 10000000f;
            IAttackTarget suitableTarget = targetList[0];
            for(int i = 0; i < targetList.Count; i++)
            {
                float dis = Vector3.Distance(pos, targetList[i].targetPosition) * targetList[i].attackPriority;
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
                currentAttackTarget.OnDead -= OnEnemyIsDead;

            currentAttackTarget = newAttackTarget;
            currentAttackTarget.OnDead += OnEnemyIsDead;
        }

        public void OnEnemyIsDead()
        {

        }
    }
}