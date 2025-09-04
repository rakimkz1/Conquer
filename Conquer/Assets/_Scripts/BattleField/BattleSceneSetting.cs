using JetBrains.Annotations;
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
        public List<EnemyWave> enemyWaves = new();

        [Serializable]
        public class EnemyWave
        {
            public float waveDuration;
            public List<WaveUnit> enemyWaves;
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
