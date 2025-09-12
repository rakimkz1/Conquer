using Monsters;
using System;
using System.Collections.Generic;
using Zenject;
using System.Linq;

namespace BattleField
{
    public class UnitsCommandKeeper
    {
        private ArmyCommandHandler _commandHandler;
        private List<ArmyCommandTypes> _enemyCommandKeeper = Enumerable.Repeat(ArmyCommandTypes.Defence, 5).ToList();
        private List<ArmyCommandTypes> _playerCommandKeeper = Enumerable.Repeat(ArmyCommandTypes.Defence, 5).ToList();
        [Inject(Id = "playerEnterToBattleInRow")]private EnterToBattleInRowHandler _playerEnterHandler;
        [Inject(Id = "enemyEnterToBattleInRow")]private EnterToBattleInRowHandler _enemyEnterHandler;
        public event Action<ArmyCommandTypes, MonsterType> OnPlayerCommand;
        public event Action<ArmyCommandTypes, MonsterType> OnEnemyCommand;
        [Inject]
        private void Construct(ArmyCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
            Init();
        }

        private void Init()
        {
            _commandHandler.OnEnemyCommand += ListenEnemyCommand;
            _commandHandler.OnPlayerCommand += ListenPlayerCommand;
        }

        private void ListenPlayerCommand(MonsterType type, ArmyCommandTypes commandType)
        {
            CheckIsPlayerEnterBattle(_playerCommandKeeper[(int)type], commandType, type);
            _playerCommandKeeper[(int)type] = commandType;
            OnPlayerCommand?.Invoke(commandType, type);
        }

        private void CheckIsPlayerEnterBattle(ArmyCommandTypes currentCommand, ArmyCommandTypes newCommand, MonsterType type)
        {
            if (currentCommand == ArmyCommandTypes.Retreat && newCommand == ArmyCommandTypes.Attack || currentCommand == ArmyCommandTypes.Retreat && newCommand == ArmyCommandTypes.Defence)
                _playerEnterHandler.RequestToEnterBattle(type);
        }

        private void ListenEnemyCommand(MonsterType type, ArmyCommandTypes commandType)
        {
            CheckIsEnemyEnterBattle(_enemyCommandKeeper[(int)type], commandType, type);
            _enemyCommandKeeper[(int)type] = commandType;
            OnEnemyCommand?.Invoke(commandType, type);
        }
        private void CheckIsEnemyEnterBattle(ArmyCommandTypes currentCommand, ArmyCommandTypes newCommand, MonsterType type)
        {
            if (currentCommand == ArmyCommandTypes.Retreat && newCommand == ArmyCommandTypes.Attack || currentCommand == ArmyCommandTypes.Retreat && newCommand == ArmyCommandTypes.Defence)
                _enemyEnterHandler.RequestToEnterBattle(type);
        }

        public ArmyCommandTypes GetCommand(MonsterType monsterType, bool isEnemy)
        {
            if (isEnemy)
                return _enemyCommandKeeper[(int)monsterType];
            return _playerCommandKeeper[(int)monsterType];
        }
    }
}