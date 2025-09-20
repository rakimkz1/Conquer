using Cysharp.Threading.Tasks;
using Game_Setup;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace BattleField
{
    public class LevelBuilder
    {
        private DataTransferScene _dataTransfer;
        private ResourceManager _resourceManager;
        public BattleSceneSetting CurrentSceneSettings { get; private set; }
        private PlayerStartProperties _playerStartProperties;
        private ArmyCommandHandler _commandHandler;
        private List<MonsterIdelData> _selectedUnitList;
        private ExtractorFactory _extractorFactory;

        [Inject]
        public LevelBuilder(DataTransferScene dataTransfer, ResourceManager resourceManager,BattleSceneSetting sceneSetting, ArmyCommandHandler commandHandler, ExtractorFactory extractorFactory)
        {
            _dataTransfer = dataTransfer;
            _resourceManager = resourceManager;
            CurrentSceneSettings = sceneSetting;
            _commandHandler = commandHandler;
            _extractorFactory = extractorFactory;
            Initialize();
        }

        private void Initialize()
        {
            string key = SceneTransferKeys.CURRENT_BATTLE_SETTING;
            //CurrentSceneSettings = _dataTransfer.Get<BattleSceneSetting>(key) as BattleSceneSetting;
            _resourceManager.LoadAsset<PlayerStartProperties>(_resourceManager.so_Keys.GetKey(PrefabKey.PlayerStartProperties),item =>
            {
                _playerStartProperties = item;
                SpawnExtractors();
            });
        }

        public void SetAllMonsterUnit(List<MonsterIdelData> monsterIdelDatas)
        {
            _selectedUnitList = monsterIdelDatas;
        }
        public List<MonsterIdelData> GetSelectedUnits() => _selectedUnitList;

        public async UniTask SpawnExtractors()
        {
            int extractorNumber = _playerStartProperties.PlayerExtractorNumber;
            int enemyExtractorNumber = CurrentSceneSettings.enemyExtractorNumber;

            for(int i = 0;i < extractorNumber; i++)
                await _extractorFactory.Create(false);

            for (int i = 0; i < enemyExtractorNumber; i++)
                await _extractorFactory.Create(true);
        }
    }
}