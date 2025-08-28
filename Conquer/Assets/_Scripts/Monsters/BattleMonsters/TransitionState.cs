using System;

namespace Monsters
{
    public class TransitionState
    {
        public Func<bool> Condition;
        public IBattleMonsterState TargetState;

        public TransitionState(IBattleMonsterState targetState, Func<bool> condition)
        {
            Condition = condition;
            TargetState = targetState;
        }
    }
}
