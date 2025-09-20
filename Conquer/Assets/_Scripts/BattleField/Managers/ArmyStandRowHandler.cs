using Monsters;
using System;
using System.Collections.Generic;
using UnityEngine;

public partial class ArmyStandRowHandler : MonoBehaviour
{
    public int maxUnitsInRow;
    public float spaceBetweenUnitInRow;
    public float spaceBetweenRows;
    public List<Row> rows = new List<Row>();
    public event Action OnArmyRowChanged;

    public Row Add(BattleMonster monster)
    {
        if (monster == null) return null;
        OnArmyRowChanged?.Invoke();
        for(int i = 0; i < rows.Count; i++)
        {
            bool isAdded = rows[i].AddMember(monster);
            if (isAdded)
                return rows[i];
        }
        int newRowIndex = FindNewRowIndex(monster);
        Row row = new Row(monster.monsterType, maxUnitsInRow);
        row.AddMember(monster);
        rows.Insert(newRowIndex, row);
        return row;
    }

    public Vector3 GetPosition(BattleMonster monster, Row targetRow)
    {
        int rowsOder = rows.IndexOf(targetRow);
        int monsterOrder = rows[rowsOder].rowMembersOrder.IndexOf(monster);

        Vector3 rowPostionInArmy = (rows.Count - rowsOder) * spaceBetweenRows * Vector3.right;
        Vector3 positionInRow = (spaceBetweenUnitInRow * targetRow.rowMembersOrder.Count * 0.5f - monsterOrder * spaceBetweenUnitInRow) * Vector3.up;
        return transform.position + positionInRow + rowPostionInArmy;
    }

    public void Remove(Row targetRow, BattleMonster monster)
    {
        if (monster == null) return;
        int index = rows.IndexOf(targetRow);
        rows[index].Remove(monster);
        if (rows[index].rowMembersOrder.Count == 0)
            rows.RemoveAt(index);
        OnArmyRowChanged?.Invoke();
    }

    private int FindNewRowIndex(BattleMonster monster)
    {
        for(int i = 0; i < rows.Count; i++)
        {
            if(monster.monsterType < rows[i].type)
                return i;
        }
        return rows.Count;
    }
}
