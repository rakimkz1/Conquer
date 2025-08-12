using Monsters;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterCollection", menuName = "ScriptableObjects/Monster/MonsterCollection")]
public class MonsterCollection : ScriptableObject
{
    public List<MonsterIdelData> monsterUnitList = new List<MonsterIdelData>();

    public void AddUnit(MonsterIdel unitTarget)
    {
        MonsterIdelData data = new MonsterIdelData(unitTarget.monsterLevel, unitTarget.monsterType);
        monsterUnitList.Add(data);
    }
    public void RemoveUnit(MonsterIdel unitTarget)
    {
        MonsterIdelData data = new MonsterIdelData(unitTarget.monsterLevel, unitTarget.monsterType);
        monsterUnitList.Remove(data);
    }
    [ContextMenu("ClearAll")]
    public void Clear()
    {
        monsterUnitList.Clear();
    }
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