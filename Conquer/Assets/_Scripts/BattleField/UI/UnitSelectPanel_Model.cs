using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleField
{
    public class UnitSelectPanel_Model : Model
    {
        public List<MonsterIdelData> monstersList;
        public int allowedMonsterPlace;
        private SaveManager saveManager;
        private SaveData saveData;
        private BattleSceneSetting so_currentBattleSetting;
        private DataTransferScene _dataTransfer;
        public UnitSelectPanel_Model(SaveManager saveManager, DataTransferScene dataTransfer, BattleSceneSetting battleSetting)
        {
            this.saveManager = saveManager;
            _dataTransfer = dataTransfer;
            so_currentBattleSetting = battleSetting;
            SetupData();
        }

        private void SetupData()
        {
            saveData = saveManager.Load();
            monstersList = saveData.Get<List<MonsterIdelData>>(SaveDataKeys.MONSTER_IDEL_DATA_LIST, out bool isContain);
            //so_currentBattleSetting = _dataTransfer.Get<BattleSceneSetting>(SceneTransferKeys.CURRENT_BATTLE_SETTING) as BattleSceneSetting;
            if (!isContain)
                throw new Exception("MonsterIdelData is empty you bitch");
            if (so_currentBattleSetting == null)
                throw new Exception("Current Battle Setting is null you bitch");

            SetBattleSettingsToProperties();
        }

        private void SetBattleSettingsToProperties()
        {
            allowedMonsterPlace = so_currentBattleSetting.allowedMonstersNumber;
        }
    }
}
