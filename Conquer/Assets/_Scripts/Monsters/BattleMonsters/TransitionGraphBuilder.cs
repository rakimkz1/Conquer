using System;
using BattleField;
using UnityEngine;
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
        private MoveToDefencePosition _moveDefencePosition;
        private MoveToTargetState _moveToTargetState;
        private ReturnToPositionState _returnToPositionState;
        private TraceTargetFromKeepingPosition _traceTargetFromKeepingPosition;

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
            _moveDefencePosition = new MoveToDefencePosition();
            _moveToTargetState = new MoveToTargetState();
            _returnToPositionState = new ReturnToPositionState();
            _traceTargetFromKeepingPosition = new TraceTargetFromKeepingPosition();
            _stateMachine.SwichState(_moveDefencePosition);
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
            _stateMachine.AddAnyTransition(_moveDefencePosition, () =>
            {
                return _stateMachine.currentArmyCommand == ArmyCommandTypes.Defence && _stateMachine.currentState != _defenceState;
            });
            _stateMachine.AddAnyTransition(_idelState, () =>
            {
                return _stateMachine.currentArmyCommand == ArmyCommandTypes.KeepPosition && _stateMachine.currentState != _returnToPositionState && _stateMachine.currentState != _traceTargetFromKeepingPosition;
            });
            _stateMachine.AddTransition(_attackPreparationState, _attackState, () =>
            {
                return _attackPreparationState.IsReady;
            });
            _stateMachine.AddTransition(_moveDefencePosition, _defenceState, () =>
            {
                return _monster.IsOnDefencePosition();
            });
            _stateMachine.AddTransition(_moveDefencePosition, _moveDefencePosition, () =>
            {
                return _monster.isRowPlaceChanged;
            });
            _stateMachine.AddTransition(_defenceState, _moveDefencePosition, () =>
            {
                return _monster.isRowPlaceChanged;
            });
            _stateMachine.AddTransition(_idelState, _traceTargetFromKeepingPosition, () =>
            {
                return _stateMachine.currentArmyCommand == ArmyCommandTypes.KeepPosition && _monster.IsTargetProvocationDistance();
            });
            _stateMachine.AddTransition(_traceTargetFromKeepingPosition, _returnToPositionState, () =>
            {
                return _stateMachine.currentArmyCommand == ArmyCommandTypes.KeepPosition && !_monster.IsTargetInTracingDistance();
            });
            _stateMachine.AddTransition(_returnToPositionState, _idelState, () =>
            {
                return _stateMachine.currentArmyCommand == ArmyCommandTypes.KeepPosition && _monster.isMonsterInKeepingPosition;
            });
        }
    }
}