using Monsters;
using System.Collections.Generic;

public class Row
{
    public List<BattleMonster> rowMembersOrder = new();
    public MonsterType type;
    public int maxMemberNumber;

    public Row (MonsterType type, int maxMemberNumber)
    {
        this.type = type;
        this.maxMemberNumber = maxMemberNumber;
    } 
    public bool AddMember(BattleMonster monster)
    {
        if (rowMembersOrder.Count == maxMemberNumber || monster.monsterType != type)
            return false;
        for(int i = 0; i < rowMembersOrder.Count; i++)
        {
            if(monster.transform.position.y > rowMembersOrder[i].transform.position.y)
            {
                rowMembersOrder.Insert(i, monster);
                monster.OnDead += Remove;
                return true;
            }
        }
        rowMembersOrder.Add(monster);
        monster.OnDead += Remove;
        return true;
    }
    public void Remove(IAttackTarget monster)
    {
        monster.OnDead -= Remove;
        rowMembersOrder.Remove(monster as BattleMonster);
    }
}