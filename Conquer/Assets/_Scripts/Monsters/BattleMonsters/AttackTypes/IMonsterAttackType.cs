using UnityEngine;

namespace Monsters
{
    public interface IMonsterAttackType 
    {
        void Attack();
        void InitAttack(IAttackTarget attackTarget, Vector3 monsterPosition, MonsterIdelData data);
        void InitAttack(Vector3 targetArea, Vector3 monsterPosition, MonsterIdelData data);
        bool isEnemy { get; set; }
        float Damage {  get; set; }
    }
}
