using System;
using BattleField;
namespace Monsters
{
    public class TransitionGraphBuilder
    {
        private BattleMonsterStateMachine _stateMachine;
        private BattleMonster _monster;
        private IdelState _idelState;
        private AttackState _attackState;
        private AttackPreparationState _attackPreparationState;
        private DefenceState _defenceState;
        private MoveToTargetState _moveToTargetState;
        private ReturnToPositionState _returnToPositionState;

        public TransitionGraphBuilder(BattleMonsterStateMachine stateMachine, BattleMonster monster)
        {
            _stateMachine = stateMachine;
            _monster = monster;
            SetStateProperties();
            SetTransitions();
        }
        private void SetStateProperties()
        {
            _idelState = new IdelState();
            _attackState = new AttackState();
            _attackPreparationState = new AttackPreparationState();
            _defenceState = new DefenceState();
            _moveToTargetState = new MoveToTargetState();
            _returnToPositionState = new ReturnToPositionState();
            _stateMachine.SwichState(_moveToTargetState);
        }

        private void SetTransitions()
        {
            _stateMachine.AddAnyTransition(_attackPreparationState, () =>
            {
                return _monster.IsTargetAttackRange() && _monster.isReadyToAttack;
            });
            _stateMachine.AddAnyTransition(_moveToTargetState, () =>
            {
                return _stateMachine.currentArmyCommand == ArmyCommandTypes.Attack && _stateMachine.currentState != _attackPreparationState && _stateMachine.currentState != _attackState;
            });
            _stateMachine.AddTransition(_attackPreparationState, _attackState, () =>
            {
                return _attackPreparationState.IsReady;
            });
        }
    }
}