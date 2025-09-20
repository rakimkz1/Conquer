using System;

namespace BattleField
{
    public class TransitionState
    {
        public Func<bool> Condition;
        public IEnemyAIState TargetState;

        public TransitionState(IEnemyAIState targetState, Func<bool> condition)
        {
            Condition = condition;
            TargetState = targetState;
        }
    }
}
