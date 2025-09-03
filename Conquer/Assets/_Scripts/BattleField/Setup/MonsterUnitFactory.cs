using Monsters;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BattleField
{
    public class MonsterUnitFactory
    {
        private DiContainer _container;
        private GameObject _monsterPrefab;
        private List<BattleMonsterPreset> so_monsterPreset;
        [Inject(Id = "playerArmyRow")] private ArmyStandRowHandler playerArmyRow;
        [Inject(Id = "enemyArmyRow")] private ArmyStandRowHandler enemyArmyRow;
        public MonsterUnitFactory(DiContainer container, GameObject monsterPrefab, List<BattleMonsterPreset> list)
        {
            _container = container;
            _monsterPrefab = monsterPrefab;
            so_monsterPreset = list;
        }

        public void Create(bool isEnemy, MonsterIdelData type, Vector3 position)
        {
            GameObject target = _container.InstantiatePrefab(_monsterPrefab);
            BattleMonster monster = target.GetComponent<BattleMonster>();

            monster.isEnemyUnit = isEnemy;
            monster.rowHandler = isEnemy ? enemyArmyRow : playerArmyRow;
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