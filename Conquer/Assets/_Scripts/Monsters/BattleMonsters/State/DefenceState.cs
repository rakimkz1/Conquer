using UnityEngine;

namespace Monsters
{
    public class DefenceState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            
        }

        public void OnExit(BattleMonster target)
        {
            target.ExitFromRow();
        }

        public void OnWork(BattleMonster target)
        {

        }
    }
}