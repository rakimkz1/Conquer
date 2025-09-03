using UnityEngine;

namespace Monsters
{
    public class TraceTargetFromKeepingPosition : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            Debug.Log("TraceState Enter");
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