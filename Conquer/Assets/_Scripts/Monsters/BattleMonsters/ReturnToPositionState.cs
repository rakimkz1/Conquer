using System;

namespace Monsters
{
    public class ReturnToPositionState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            throw new NotImplementedException();
        }

        public void OnExit(BattleMonster target)
        {
            throw new NotImplementedException();
        }

        public void OnWork(BattleMonster target)
        {
            target.ReturnToPosition();
        }
    }
}
