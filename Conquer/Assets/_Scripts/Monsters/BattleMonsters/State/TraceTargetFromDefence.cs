using UnityEngine;

namespace Monsters
{
    public class TraceTargetFromDefence : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.viewMonster.animationManager?.MoveAnimation();
        }

        public void OnExit(BattleMonster target)
        {
        }

        public void OnWork(BattleMonster target)
        {
            target.attackHandler.MoveToTarget();
        }
    }
}
