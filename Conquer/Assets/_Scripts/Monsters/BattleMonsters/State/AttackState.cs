using UnityEngine;

namespace Monsters
{
    public class AttackState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.Attack();
        }

        public void OnExit(BattleMonster target)
        {
            Debug.Log("Attack Exit");
        }

        public void OnWork(BattleMonster target)
        {

        }
    }
}