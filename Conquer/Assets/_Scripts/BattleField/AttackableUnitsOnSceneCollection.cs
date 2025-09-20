using Monsters;
using System;
using System.Collections.Generic;
using UniRx;

namespace BattleField
{
    public class AttackableUnitsOnSceneCollection
    {
        public List<IAttackTarget> enemyUnits {  get; private set; }
        public List<IAttackTarget> playerUnits { get; private set; }

        public event Action<IAttackTarget> OnEnemyAdded;
        public event Action <IAttackTarget> OnEnemyRemoved;
        public event Action <IAttackTarget> OnPlayerAdded;
        public event Action <IAttackTarget> OnPlayerRemoved;

        public AttackableUnitsOnSceneCollection()
        {
            enemyUnits = new List<IAttackTarget>();
            playerUnits = new List<IAttackTarget>();
        }

        public void AddEnemyUnit(IAttackTarget enemyUnit)
        {
            enemyUnits.Add(enemyUnit);
            OnEnemyAdded?.Invoke(enemyUnit);
        }

        public void AddPlayerUnit(IAttackTarget playerUnit)
        {
            playerUnits.Add(playerUnit);
            OnPlayerAdded?.Invoke(playerUnit);
        }

        public void RemoveEnemyUnit(IAttackTarget enemyUnit)
        {
            enemyUnits.Remove(enemyUnit);
            OnEnemyRemoved?.Invoke(enemyUnit);
        }

        public void RemovePlayerUnit(IAttackTarget playerUnit)
        {
            playerUnits.Remove(playerUnit);
            OnPlayerRemoved?.Invoke(playerUnit);
        }
    }
}