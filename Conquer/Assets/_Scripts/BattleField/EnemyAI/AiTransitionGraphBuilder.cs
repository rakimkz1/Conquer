using System;

namespace BattleField
{
    public class AiTransitionGraphBuilder
    {
        private AttackAIState _attackState;
        private DefenceAIState _defenceState;
        private RetraeateAIState _retreateState;
        private AIStateMachine _stateMachine;
        private EnemyAI _enemyAI;
        public AiTransitionGraphBuilder(AIStateMachine stateMachine, EnemyAI enemyAI)
        {
            _attackState = new AttackAIState();
            _defenceState = new DefenceAIState();
            _retreateState = new RetraeateAIState();
            _stateMachine = stateMachine;
            _stateMachine.SwitchState(_defenceState);
            _enemyAI = enemyAI;
            SetTranstions();
        }

        private void SetTranstions()
        {
            _stateMachine.AddTranstion(_defenceState, _attackState, () =>
            {
                return _enemyAI.IsSureOverPower() && _stateMachine._isAllowedToSwitchState;
            });
            _stateMachine.AddTranstion(_defenceState, _retreateState, () =>
            {
                return _enemyAI.IsSureMoveToRetreat() && _stateMachine._isAllowedToSwitchState;
            });
            _stateMachine.AddTranstion(_retreateState, _defenceState, () =>
            {
                return _enemyAI.IsSureMoveToDefence() && _stateMachine._isAllowedToSwitchState;
            });
            _stateMachine.AddTranstion(_attackState, _defenceState, () =>
            {
                return _enemyAI.IsSureMoveToDefence() && _stateMachine._isAllowedToSwitchState;
            });
        }
    }
}