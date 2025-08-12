using Monsters;
using System;

namespace MainHUB.Extractor
{
    [Serializable]
    public class SpawnPanel_Model : Model
    {
        public int manaCost;
        public MonsterCollection.MonsterIdelData data;
        public PrefabKey prefabKey;
    }
}