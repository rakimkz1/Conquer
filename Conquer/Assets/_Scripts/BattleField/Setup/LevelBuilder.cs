
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BattleField
{
    public class LevelBuilder
    {
        private DataTransferScene _dataTransfer;
        public BattleSceneSetting CurrentSceneSettings { get; private set; }
        public List<MonsterIdelData> AllMonstersUnits { get; private set; }
        private MonsterUnitFactory _factory; 

        [Inject]
        public LevelBuilder(DataTransferScene dataTransfer, MonsterUnitFactory factory)
        {
            _dataTransfer = dataTransfer;
            _factory = factory;
            Initialize();
        }

        private void Initialize()
        {
            string key = SceneTransferKeys.CURRENT_BATTLE_SETTING;
            CurrentSceneSettings = _dataTransfer.Get<BattleSceneSetting>(key) as BattleSceneSetting;
        }

        public void SetAllMonsterUnit(List<MonsterIdelData> list)
        {
            AllMonstersUnits = new List<MonsterIdelData>();
            AllMonstersUnits.AddRange(list);
            SpawnMonsters();
        }
        public void SpawnMonsters()
        {
            for(int i = 0; i < AllMonstersUnits.Count; i++)
            {
                _factory.Create(false, AllMonstersUnits[i], Vector3.zero);
            }
        }
    }
}
