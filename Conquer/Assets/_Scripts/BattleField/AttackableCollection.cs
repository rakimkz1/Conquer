using Monsters;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace BattleField
{
    public class AttackableCollection
    {
        public List<IAttackTarget> enemyUnits {  get; private set; }
        public List<IAttackTarget> playerUnits { get; private set; }

        public event Action<IAttackTarget> OnEnemyAdded;
        public event Action <IAttackTarget> OnEnemyRemoved;
        public event Action <IAttackTarget> OnPlayerAdded;
        public event Action <IAttackTarget> OnPlayerRemoved;
        public AttackableCollection()
        {
            enemyUnits = new List<IAttackTarget>();
            playerUnits = new List<IAttackTarget>();
        }
        public void AddEnemyUnit(IAttackTarget enemyUnit)
        {
            if (enemyUnits.Contains(enemyUnit))
                return;
            enemyUnits.Add(enemyUnit);
            OnEnemyAdded?.Invoke(enemyUnit);
        }
        public void AddPlayerUnit(IAttackTarget playerUnit)
        {
            if (playerUnits.Contains(playerUnit))
                return;
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