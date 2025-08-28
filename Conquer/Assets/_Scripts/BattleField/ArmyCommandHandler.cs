using System;

namespace BattleField
{
    public class ArmyCommandHandler
    {
        public event Action<ArmyCommandTypes> OnCommandToAllUnits;
    }

    public enum ArmyCommandTypes
    {
        Attack,
        KeepPosition,
        Defence,
        Retreat
    }
}
