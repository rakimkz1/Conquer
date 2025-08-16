using Monsters;
using System;

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