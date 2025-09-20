using System;
using UnityEngine;

namespace BattleField
{
    public class EnemyUnitBuymentHandler
    {
        private EnemyManaHandler _manaHandler;
        private MonsterSpawnHandler _spawnHandler;
        private LevelBuilder _levelBuilder;
        public event Action OnMonsterSpawn;
        public EnemyUnitBuymentHandler(EnemyManaHandler manaHandler, MonsterSpawnHandler spawnHandler, LevelBuilder levelBuilder)
        {
            _manaHandler = manaHandler;
            _spawnHandler = spawnHandler;
            _levelBuilder = levelBuilder;
            Init();
        }

        private void Init()
        {
            _manaHandler.OnManaAdded += BuyUnit;
        }
        private void BuyUnit(float value)
        {
            if(_levelBuilder.CurrentSceneSettings.unitManaCost <= value)
            {
                _spawnHandler.Spawn(true);
                _manaHandler.SpendMana(_levelBuilder.CurrentSceneSettings.unitManaCost);
                OnMonsterSpawn?.Invoke();
            }
        }
    }
}