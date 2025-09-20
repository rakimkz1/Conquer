using System;
using UnityEngine;
using System.Collections.Generic;
using Monsters;

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
        private Queue<BattleMonster> _unitPool = new Queue<BattleMonster>();
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

        public bool isSpawnable(bool isEnemy)
        {
            if (isEnemy && _enemyMonsterList.Count == 0)
                return false;
            if (!isEnemy && _selectedMonsterList.Count == 0)
                return false;
            return true;
        }

        public void Spawn(bool isEnemy)
        {
            if (!isSpawnable(isEnemy))
                return;
            MonsterIdelData randomUnit = GetRandomUnit(isEnemy);

            if (_unitPool.Count == 0)
            {
                BattleMonster target = _factory.Create(isEnemy, randomUnit, ref OnMonsterSpawn);
                target.OnDead += AddToPool;
            }
            else
            {
                BattleMonster target = _unitPool.Dequeue();
                _factory.Spawn(target, isEnemy, randomUnit, ref OnMonsterSpawn);
                target.OnDead += AddToPool;
            }
            OnMonsterSpawn?.Invoke();
            OnMonsterSpawn = null;
        }

        private void AddToPool(IAttackTarget target)
        {
            _unitPool.Enqueue(target as BattleMonster);
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