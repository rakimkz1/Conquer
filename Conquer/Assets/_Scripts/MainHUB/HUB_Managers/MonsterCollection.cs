using Monsters;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollection : IDisposable
{
    public List<MonsterIdelData> monsterUnitList = new List<MonsterIdelData>();

    private SaveManager saveManager;
    private SaveData saveData;
    public MonsterCollection(SaveManager saveManager)
    {
        this.saveManager = saveManager;
        saveData = saveManager.Load();
        SetMonsterUnitData();
    }

    private void SetMonsterUnitData()
    {
        List<MonsterIdelData> dataList = saveData.Get<List<MonsterIdelData>>(SaveDataKeys.MONSTER_IDEL_DATA_LIST, out bool isContain);
        if (!isContain)
        {
            saveData.Set<List<MonsterIdelData>>(SaveDataKeys.MONSTER_IDEL_DATA_LIST, new List<MonsterIdelData>());
            return;
        }
        monsterUnitList = dataList;
    }

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
    public void SaveData()
    {
        saveManager.Save(saveData);
    }
    [ContextMenu("ClearAll")]
    public void Clear()
    {
        monsterUnitList.Clear();
    }

    public void Dispose()
    {
        SaveData();
    }
}