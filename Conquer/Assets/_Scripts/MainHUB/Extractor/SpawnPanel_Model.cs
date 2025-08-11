using Monsters;
using System;
using System.Collections;
using UnityEngine;

namespace MainHUB.Extractor
{
    [Serializable]
    public class SpawnPanel_Model : Model
    {
        public int manaCost;
        public int monsterLevel;
        public MonsterType monsterType;
        public PrefabKey prefabKey;
    }
}