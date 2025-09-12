using Cysharp.Threading.Tasks;
using Monsters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleField
{
    public class EnterToBattleInRowHandler : MonoBehaviour
    {
        public List<Row> retreatedUnitsList = new List<Row>();
        public bool[] isMonsterOrderedEnter = new bool[Enum.GetValues(typeof(MonsterType)).Length];
        public Dictionary<BattleMonster, Action<Vector3>> onAllowedEnterMap = new Dictionary<BattleMonster, Action<Vector3>>();
        public int maxUnitsInRow;
        public float spaceBetweenUnitInRow;
        public float rowSpawnTime;
        private bool isSpawningColdown = false;
        public void Add(BattleMonster monster)
        {
            for(int  i = 0; i < retreatedUnitsList.Count; i++)
            {
                if (retreatedUnitsList[i].rowMembersOrder.Count < maxUnitsInRow && retreatedUnitsList[i].type == monster.monsterType)
                {
                    retreatedUnitsList[i].AddMember(monster);
                    if (onAllowedEnterMap.ContainsKey(monster))
                    {
                        onAllowedEnterMap[monster] += monster.retreatHandler.AllowedEnterToBattle;
                        return;
                    }
                    onAllowedEnterMap.Add(monster, monster.retreatHandler.AllowedEnterToBattle);
                    return;
                }
            }
            AddNewRow(monster);
            return;
        }

        public void RequestToEnterBattle(MonsterType monsterType)
        {
            isMonsterOrderedEnter[(int)monsterType] = true;
            CheckAllRequests();
        }

        private async UniTask CheckAllRequests()
        {
            if (isSpawningColdown)
                return;
            isSpawningColdown = true;
            for(int  i = 0; i < isMonsterOrderedEnter.Length; i++)
            {
                if (isMonsterOrderedEnter[i])
                {
                    await AllowRowToEnter((MonsterType)i);
                    isMonsterOrderedEnter[i] = false;
                    i = -1;
                }
            }
            isSpawningColdown = false;
        }

        private async UniTask AllowRowToEnter(MonsterType type)
        {
            for(int i = 0; i < retreatedUnitsList.Count; i++)
            {
                if (retreatedUnitsList[i].type == type)
                {
                    SendStartPositionToUnits(retreatedUnitsList[i]);
                    retreatedUnitsList.Remove(retreatedUnitsList[i]);
                    i--;
                    await UniTask.Delay((int)(rowSpawnTime * 1000f));
                }
            }
        }

        private void SendStartPositionToUnits(Row row)
        {
            for(int i = 0; i < row.rowMembersOrder.Count; i++)
            {
                Vector3 pos = transform.position + (spaceBetweenUnitInRow * (row.rowMembersOrder.Count - 1) * 0.5f - i * spaceBetweenUnitInRow) * Vector3.up;
                onAllowedEnterMap[row.rowMembersOrder[i]]?.Invoke(pos);
            }
            Debug.Log($"Allow To Enter {row.type.ToString()}");
        }

        private void AddNewRow(BattleMonster monster)
        {
            Row row = new Row(monster.monsterType, maxUnitsInRow);
            row.AddMember(monster);
            retreatedUnitsList.Add(row);
            if(onAllowedEnterMap.ContainsKey(monster))
            {
                onAllowedEnterMap[monster] += monster.retreatHandler.AllowedEnterToBattle;
                return;
            }
            onAllowedEnterMap.Add(monster, monster.retreatHandler.AllowedEnterToBattle);
        }

        [ContextMenu("CkeckInfo")]
        public void CheckInfo()
        {
            for(int i=0;i<retreatedUnitsList.Count; i++)
            {
                string answer = "";
                for(int j =0; j < retreatedUnitsList[i].rowMembersOrder.Count; j++)
                {
                    answer += $" {retreatedUnitsList[i].rowMembersOrder[j].monsterType.ToString()} ";
                }
                Debug.Log(answer);
            }
        }
    }
}