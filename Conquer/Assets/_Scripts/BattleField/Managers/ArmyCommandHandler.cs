using Monsters;
using System;
using System.Collections.Generic;

namespace BattleField
{
    public class ArmyCommandHandler
    {
        public event Action<MonsterType, ArmyCommandTypes> OnEnemyCommand;
        public event Action<MonsterType, ArmyCommandTypes> OnPlayerCommand;
        public void SayCommandEnemy(MonsterType type, ArmyCommandTypes commandType)
        {
            OnEnemyCommand?.Invoke(type, commandType);
        }
        public void SayCommandPlayer(MonsterType type, ArmyCommandTypes commandType)
        {
            OnPlayerCommand?.Invoke(type, commandType);
        }
    }

    public enum ArmyCommandTypes
    {
        Attack,
        KeepPosition,
        Defence,
        Retreat
    }
}
