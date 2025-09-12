using Monsters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleField
{
    [CreateAssetMenu(fileName = "BattleFieldSceneSetting", menuName = "ScriptableObjects/BattleField/SceneSetting")]
    public class BattleSceneSetting : ScriptableObject 
    {
        public int allowedMonstersNumber;
        public int enemysWallHealth;
        public int enemyExtractorNumber;
        public int unitManaCost;
        public List<WaveUnit> enemyWaves = new();
        
        public List<MonsterIdelData> GetMonstersList()
        {
            List<MonsterIdelData> list = new();
            for(int i = 0; i< enemyWaves.Count; i++)
            {
                for(int j = 0; j < enemyWaves[i].unitNumber; j++)
                {
                    MonsterIdelData data = new MonsterIdelData(enemyWaves[i].level, enemyWaves[i].monsterType);
                    list.Add(data);
                }
            }
            return list;
        }
        
        [Serializable]
        public class WaveUnit
        {
            public int level;
            public MonsterType monsterType;
            public int unitNumber;
        }
    }
}
