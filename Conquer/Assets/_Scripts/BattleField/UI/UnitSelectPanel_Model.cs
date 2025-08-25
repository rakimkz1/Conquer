using System;
using System.Collections.Generic;

namespace BattleField
{
    public class UnitSelectPanel_Model : Model
    {
        public List<MonsterIdelData> monstersList;
        public int allowedMonsterPlace;
        private SaveManager saveManager;
        private SaveData saveData;
        private DataTransferScene dataTransfer;
        private BattleSceneSetting so_currentBattleSetting;

        public UnitSelectPanel_Model(SaveManager saveManager, DataTransferScene dataTransfer)
        {
            this.saveManager = saveManager;
            this.dataTransfer = dataTransfer;
            SetupData();
        }

        private void SetupData()
        {
            saveData = saveManager.Load();
            monstersList = saveData.Get<List<MonsterIdelData>>(SaveDataKeys.MONSTER_IDEL_DATA_LIST, out bool isContain);
#if UNITY_EDITOR
            so_currentBattleSetting = new BattleSceneSetting();
#endif
      //      so_currentBattleSetting = dataTransfer.data[SceneTransferKeys.CURRENT_BATTLE_SETTING] as BattleSceneSetting;
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
