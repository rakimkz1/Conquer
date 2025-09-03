using System;
using UnityEngine;

namespace Monsters
{
    public class ReturnToPositionState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            Debug.Log("Return to position enter");
        }

        public void OnExit(BattleMonster target)
        {

        }

        public void OnWork(BattleMonster target)
        {
            target.ReturnToPosition();
        }
    }
}
