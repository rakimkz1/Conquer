using UnityEngine;

namespace Monsters
{
    public class WaitEnterToBattle : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.RequestEnterToBattle();
        }

        public void OnExit(BattleMonster target)
        {
            target.ResetAllowmentEnter();
        }

        public void OnWork(BattleMonster target)
        {

        }
    }
}