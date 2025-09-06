using Cysharp.Threading.Tasks;
using Game_Setup;
using Monsters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace BattleField
{
    public class MonsterUnitFactory
    {
        private DiContainer _container;
        private GameObject _monsterPrefab;
        private List<BattleMonsterPreset> so_monsterPreset;
        private SaveManager _saveManager;
        private SaveData _saveData;
        private ResourceManager _resourceManager;
        private PlayerStartProperties so_playerStartProperties;
        [Inject(Id = "playerArmyRow")] private ArmyStandRowHandler playerArmyRow;
        [Inject(Id = "enemyArmyRow")] private ArmyStandRowHandler enemyArmyRow;
        [Inject(Id = "playerRetreatPoint")] private Transform playerRetreatPoint;
        [Inject(Id = "enemyRetreatPoint")] private Transform enemyRetreatPoint;
        [Inject(Id = "playerEnterToBattleInRow")] private EnterToBattleInRowHandler playerEnterToBattle;
        [Inject(Id = "enemyEnterToBattleInRow")] private EnterToBattleInRowHandler enemyEnterToBattle;
        public MonsterUnitFactory(DiContainer container, GameObject monsterPrefab, List<BattleMonsterPreset> list, SaveManager saveManager, ResourceManager resourceManager)
        {
            _container = container;
            _monsterPrefab = monsterPrefab;
            so_monsterPreset = list;
            _resourceManager = resourceManager;
            _saveManager = saveManager;
            _saveData = _saveManager.Load();
            LoadResources();
        }


        public void Create(bool isEnemy, MonsterIdelData type, Vector3 position)
        {
            GameObject target = _container.InstantiatePrefab(_monsterPrefab);
            BattleMonster monster = target.GetComponent<BattleMonster>();

            monster.isEnemyUnit = isEnemy;
            monster.rowHandler = isEnemy ? enemyArmyRow : playerArmyRow;
            monster.outOfBattlePoint = isEnemy ? enemyRetreatPoint : playerRetreatPoint;
            monster.enterToBattleInRowHandler = isEnemy ? enemyEnterToBattle : playerEnterToBattle;
            SetMonsterHealProperties(monster);
            SetMonsterSetting(type, monster);
            target.transform.position = position;
        }


        private void SetMonsterSetting(MonsterIdelData type, BattleMonster monster)
        {
            BattleMonsterPreset monsterPreset = FindMonsterPreset(type);
            monster.monsterLevel = type.monsterLevel;
            monster.monsterType = type.monsterType;
            monster.provocationDistance = monsterPreset.provocationDistance;
            monster.maxTracingDistance = monsterPreset.maxTracingDistance;
            monster.speed = monsterPreset.speed;
            monster.attackDistance = monsterPreset.attackDistance;
            monster.attackSpeed = monsterPreset.attackSpeed;
            monster.attackPreparationTime = monsterPreset.attackPreparationTime;
            monster.attackPriority = monsterPreset.attackPriority;
            monster.healthHandler.maxHealth = monsterPreset.maxHealth;
        }
        private void SetMonsterHealProperties(BattleMonster monster)
        {
            float retreatHealAmount = _saveData.Get<float>(SaveDataKeys.PLAYER_RETREAT_HEAL_AMOUNT, out bool isContainHealAmount);
            float retreatHealColdown = _saveData.Get<float>(SaveDataKeys.PLAYER_RETREAT_HEAL_COLDOWN, out bool isContainHealColdown);
            if (!isContainHealAmount)
            {
                retreatHealAmount = so_playerStartProperties.RetreatHealAmount;
                _saveData.Set(SaveDataKeys.PLAYER_RETREAT_HEAL_AMOUNT, retreatHealAmount);
            }
            if (!isContainHealColdown)
            {
                retreatHealColdown = so_playerStartProperties.RetreatHealColdown;
                _saveData.Set(SaveDataKeys.PLAYER_RETREAT_HEAL_COLDOWN, retreatHealColdown);
            }
            monster.healthHandler = new HealthHandler();
            monster.healthHandler.retreadHealAmount = retreatHealAmount;
            monster.healthHandler.retreatHealColdown = retreatHealColdown;
        }
        private async UniTask LoadResources()
        {
            _resourceManager.LoadAsset<PlayerStartProperties>(_resourceManager.so_Keys.GetKey(PrefabKey.PlayerStartProperties),value =>
            {
                so_playerStartProperties = value;
            });
        }

        private BattleMonsterPreset FindMonsterPreset(MonsterIdelData type)
        {
            for(int i = 0; i < so_monsterPreset.Count; i++)
            {
                if (Equals(so_monsterPreset[i].data, type))
                    return so_monsterPreset[i];
            }
            return null;
        }
    }
}