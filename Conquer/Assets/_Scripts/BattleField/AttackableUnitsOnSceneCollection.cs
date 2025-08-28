using Monsters;
using System.Collections.Generic;

namespace BattleField
{
    public class AttackableUnitsOnSceneCollection
    {
        public List<IAttackTarget> enemyUnits { get; private set; } 
        public List<IAttackTarget> playerUnits { get; private set; }

        public AttackableUnitsOnSceneCollection()
        {
            enemyUnits = new List<IAttackTarget>();
            playerUnits = new List<IAttackTarget>();
        }

        public void AddEnemyUnit(IAttackTarget enemyUnit) => enemyUnits.Add(enemyUnit);
        public void AddPlayerUnit(IAttackTarget playerUnit) => playerUnits.Add(playerUnit);

        public void RemoveEnemyUnit(IAttackTarget enemyUnit) => enemyUnits.Remove(enemyUnit);
        public void RemovePlayerUnit(IAttackTarget playerUnit) => playerUnits.Remove(playerUnit);
    }
}