using Monsters;
using System;

namespace MainHUB.Extractor
{
    [Serializable]
    public class SpawnPanel_Model : Model
    {
        public int manaCost;
        public MonsterIdelData data;
        public string prefabPath;
    }
}