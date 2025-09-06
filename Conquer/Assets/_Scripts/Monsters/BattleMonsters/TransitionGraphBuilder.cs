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
        private RetreatState _retreatState;
        private OutOfBattleState _outOfBattleState;
        private WaitEnterToBattle _waitEnterToBattle;
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
            _retreatState = new RetreatState();
            _outOfBattleState = new OutOfBattleState();
            _waitEnterToBattle = new WaitEnterToBattle();
            _stateMachine.SwichState(_moveDefencePosition);
        }

        private void SetTransitions()
        {
            _stateMachine.AddAnyTransition(_attackPreparationState, () =>
            {
                return _monster.IsTargetAttackRange() && _monster.isCapableToAttack();
            });
            _stateMachine.AddAnyTransition(_moveToTargetState, () =>
            {
                return _stateMachine.currentArmyCommand == ArmyCommandTypes.Attack && _stateMachine.currentState != _attackPreparationState && _stateMachine.currentState != _attackState && _stateMachine.currentState != _waitEnterToBattle && _stateMachine.currentState != _outOfBattleState;
            });
            _stateMachine.AddAnyTransition(_moveDefencePosition, () =>
            {
                return _stateMachine.currentArmyCommand == ArmyCommandTypes.Defence && _stateMachine.currentState != _defenceState && _stateMachine.currentState != _waitEnterToBattle && _stateMachine.currentState != _outOfBattleState;
            });
            _stateMachine.AddAnyTransition(_idelState, () =>
            {
                return _stateMachine.currentArmyCommand == ArmyCommandTypes.KeepPosition && _stateMachine.currentState != _returnToPositionState && _stateMachine.currentState != _traceTargetFromKeepingPosition && _stateMachine.currentState != _outOfBattleState && _stateMachine.currentState != _waitEnterToBattle;
            });
            _stateMachine.AddAnyTransition(_retreatState, () =>
            {
                return _stateMachine.currentArmyCommand == ArmyCommandTypes.Retreat && _stateMachine.currentState != _outOfBattleState && _stateMachine.currentState != _waitEnterToBattle;
            });
            _stateMachine.AddTransition(_attackPreparationState, _attackState, () =>
            {
                return _monster.isReadyToAttack();
            });
            _stateMachine.AddTransition(_moveDefencePosition, _defenceState, () =>
            {
                return _monster.IsOnDefencePosition();
            });
            _stateMachine.AddTransition(_moveDefencePosition, _moveDefencePosition, () =>
            {
                return _monster.isRowPlaceChanged();
            });
            _stateMachine.AddTransition(_defenceState, _moveDefencePosition, () =>
            {
                return _monster.isRowPlaceChanged();
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
                return _stateMachine.currentArmyCommand == ArmyCommandTypes.KeepPosition && _monster.isMonsterInKeepingPosition();
            });
            _stateMachine.AddTransition(_retreatState, _outOfBattleState, () =>
            {
                return _monster.IsOutOfBattle();
            });
            _stateMachine.AddTransition(_outOfBattleState, _waitEnterToBattle, () =>
            {
                return _stateMachine.currentArmyCommand != ArmyCommandTypes.Retreat && _stateMachine.currentArmyCommand != ArmyCommandTypes.KeepPosition;
            });
            _stateMachine.AddTransition(_waitEnterToBattle, _moveToTargetState, () =>
            {
                return _monster.isAllowedToEnterBattle() && _stateMachine.currentArmyCommand == ArmyCommandTypes.Attack;
            });
            _stateMachine.AddTransition(_waitEnterToBattle, _moveDefencePosition, () =>
            {
                return _monster.isAllowedToEnterBattle() && _stateMachine.currentArmyCommand == ArmyCommandTypes.Defence;
            });
        }
    }
}