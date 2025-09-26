using Monsters;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BattleField
{
    public class GameOverHandler
    {
        private Base playerBase;
        private Base enemyBase;
        private SaveManager _saveManager;
        private SaveData _saveData;
        private UnitsAliveChecker _aliveChecker;
        private UnitSelectPanel_ViewModel _unitSelection;
        public event Action OnGameOver;
        public event Action OnWin;
        public event Action OnLose;
        public GameOverHandler([Inject(Id = "playerBase")] Base playerBase, [Inject(Id = "enemyBase")] Base enemyBase, SaveManager saveManager, UnitsAliveChecker aliveChecker, UnitSelectPanel_ViewModel unitSelection)
        {
            this.playerBase = playerBase;
            this.enemyBase = enemyBase;
            _saveManager = saveManager;
            _aliveChecker = aliveChecker;
            _unitSelection = unitSelection;
            Init();
        }
        private void Init()
        {
            playerBase.OnDead += Lose;
            enemyBase.OnDead += Win;
            _saveData = _saveManager.Load();
        }
        private void Win(IAttackTarget target)
        {
            OnGameOver?.Invoke();
            OnWin?.Invoke();
            SaveSurvivedUnits();
        }
        private void Lose(IAttackTarget target)
        {
            OnGameOver?.Invoke();
            OnLose?.Invoke();
            SaveSurvivedUnits();
        }
        private void SaveSurvivedUnits()
        {
            string key = SaveDataKeys.MONSTER_IDEL_DATA_LIST;
            List<MonsterIdelData> data = _aliveChecker.GetSurvivedMonsterCollection();
            data.AddRange(_unitSelection.notSelectedMonsters);
            _saveData.Set(key, data);
            _saveManager.Save(_saveData);
        }
    }
}