using System;
using Zenject;

namespace BattleField
{
    public class EnemyAI
    {
        private EnemyUnitBuymentHandler _unitBuymentHandler;
        private AIStateMachine _stateMachine;
        private AIPowerCanculator _powerCanculator;
        private EnemyCommandHandler _enemyCommand;

        public EnemyAI(EnemyUnitBuymentHandler unitBuymentHandler, EnemyCommandHandler enemyCommnad)
        {
            _unitBuymentHandler = unitBuymentHandler;
            _enemyCommand = enemyCommnad;
        }
        public void Init(AIPowerCanculator aIPowerCanculator, AIStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _powerCanculator = aIPowerCanculator;
            _unitBuymentHandler.OnMonsterSpawn += _stateMachine.CheckState;
            _enemyCommand.OnListenPlayerCommand += _stateMachine.CheckState;
        }
        public bool IsSureOverPower() => _powerCanculator.isAttackPlayer();
        public bool IsSureMoveToDefence() => _powerCanculator.isDefence();
        public bool IsSureMoveToRetreat() => _powerCanculator.isRetreat();
        public void CommandAttack() => _enemyCommand.AttackCommand();
        public void CommandKeepPosition() => _enemyCommand.KeepPositionCommand();
        public void CommandDefence() => _enemyCommand.DefenceCommand();
        public void CommandRetreat() => _enemyCommand.RetreatCommand();
    }
}
