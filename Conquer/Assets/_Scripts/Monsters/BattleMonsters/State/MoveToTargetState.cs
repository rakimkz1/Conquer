using BattleField;
using UnityEngine;

namespace Monsters
{
    public class MoveToTargetState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.FindAttackTarget();
        }

        public void OnExit(BattleMonster target)
        {
            
        }

        public void OnWork(BattleMonster target)
        {
            target.MoveToTarget();
        }
    }
}