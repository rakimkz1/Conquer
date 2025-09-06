using UnityEngine;

namespace Monsters
{
    public class OutOfBattleState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.GoOutOfBattle();
        }

        public void OnExit(BattleMonster target)
        {
            target.EnterToBattleFromRetreat();
        }

        public void OnWork(BattleMonster target)
        {

        }
    }
}