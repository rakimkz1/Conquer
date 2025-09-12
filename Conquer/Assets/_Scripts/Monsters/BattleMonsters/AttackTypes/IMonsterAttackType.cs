using UnityEngine;

namespace Monsters
{
    public interface IMonsterAttackType 
    {
        void Attack();
        void InitAttack(IAttackTarget attackTarget, Vector3 monsterPosition);
        void InitAttack(Vector3 targetArea, Vector3 monsterPosition);
        bool isEnemy { get; set; }
        float Damage {  get; set; }
    }
}
