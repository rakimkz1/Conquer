using Monsters;
using System;

public partial class MonsterCollection
{
    [Serializable]
    public struct MonsterIdelData
    {
        public int monsterLevel;
        public MonsterType monsterType;
        public MonsterIdelData(int level, MonsterType type)
        {
            monsterLevel = level;
            monsterType = type;
        }
    }
}