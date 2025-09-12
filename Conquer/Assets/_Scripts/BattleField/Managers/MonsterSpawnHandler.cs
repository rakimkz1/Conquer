using System;
using UnityEngine;
using System.Collections.Generic;

namespace BattleField
{
    public class MonsterSpawnHandler
    {
        private MonsterUnitFactory _factory;
        private BattleStarter _battleStarter;
        private UnitSelectPanel_ViewModel _unitSelection;
        private List<MonsterIdelData> _selectedMonsterList = new();
        private List<MonsterIdelData> _enemyMonsterList = new();
        private LevelBuilder _levelBuilder;
        private event Action OnMonsterSpawn;
        public MonsterSpawnHandler(MonsterUnitFactory factory, BattleStarter battleStarter, LevelBuilder levelBuilder, UnitSelectPanel_ViewModel unitSelection)
        {
            _factory = factory;
            _battleStarter = battleStarter;
            _levelBuilder = levelBuilder;
            _unitSelection = unitSelection;
            Init();
        }

        private void Init()
        {
            _battleStarter.OnBattleStart += GetSelectionList;
        }

        private void GetSelectionList()
        {
            _selectedMonsterList.AddRange(_unitSelection.selectedMonsters);
            _enemyMonsterList.AddRange(_levelBuilder.CurrentSceneSettings.GetMonstersList());
        }

        public void Spawn(bool isEnemy)
        {
            MonsterIdelData randomUnit = GetRandomUnit(isEnemy);
            _factory.Create(isEnemy, randomUnit, ref OnMonsterSpawn);
            OnMonsterSpawn?.Invoke();
            OnMonsterSpawn = null;
        }

        private MonsterIdelData GetRandomUnit(bool isEnemy)
        {
            MonsterIdelData answer;
            if (isEnemy)
            {
                answer = _enemyMonsterList[UnityEngine.Random.Range(0, _enemyMonsterList.Count)];
                _enemyMonsterList.Remove(answer);
                return answer;
            }
            answer = _selectedMonsterList[UnityEngine.Random.Range(0, _selectedMonsterList.Count)];
            _selectedMonsterList.Remove(answer);
            return answer;
        }
    }
}