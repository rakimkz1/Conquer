using UnityEngine;

namespace Monsters
{
    public class MoveToTargetState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            Debug.Log("MovementEnter");
            target.FindAttackTarget();
        }

        public void OnExit(BattleMonster target)
        {
            Debug.Log("Movement Exit");
            target.StopMoving();
        }

        public void OnWork(BattleMonster target)
        {
            target.MoveToTarget();
        }
    }
}