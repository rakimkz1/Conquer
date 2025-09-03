using System;

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

        }

        public void OnWork(BattleMonster target)
        {
            target.MoveToDefencePosition();
        }
    }
}