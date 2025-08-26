
using System;
using System.Collections.Generic;
using Zenject;

namespace BattleField
{
    public class LevelBuilder
    {
        private DataTransferScene _dataTransfer;
        public BattleSceneSetting CurrentSceneSettings { get; private set; }
        public List<MonsterIdelData> AllMonstersUnits { get; private set; }

        [Inject]
        public LevelBuilder(DataTransferScene dataTransfer)
        {
            _dataTransfer = dataTransfer;
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
        }
    }
}
