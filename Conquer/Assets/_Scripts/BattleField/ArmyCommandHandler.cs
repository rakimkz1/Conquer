using Monsters;
using System;
using System.Collections.Generic;

namespace BattleField
{
    public class ArmyCommandHandler
    {
        public Dictionary<MonsterType, Action<ArmyCommandTypes>> OnCommand = new Dictionary<MonsterType, Action<ArmyCommandTypes>>();

        public void SayCommand(MonsterType type, ArmyCommandTypes commandType)
        {
            OnCommand[type]?.Invoke(commandType);
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
