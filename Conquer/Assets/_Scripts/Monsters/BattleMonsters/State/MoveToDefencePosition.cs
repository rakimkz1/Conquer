using System;
using UnityEngine;

namespace Monsters
{
    public class MoveToDefencePosition : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.SetRow();
            target.GetDefendePosition();
        }

        public void OnExit(BattleMonster target)
        {
            if(target.stateMachine.movingToState is DefenceState == false)
            {
                target.ExitFromRow();
            }
        }

        public void OnWork(BattleMonster target)
        {
            target.MoveToDefencePosition();
        }
    }
}