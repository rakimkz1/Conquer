using UnityEngine;

namespace Monsters
{
    public class AttackState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            Debug.Log("Attack Enter");
        }

        public void OnExit(BattleMonster target)
        {
            Debug.Log("Attack Exit");
        }

        public void OnWork(BattleMonster target)
        {
            Debug.Log("Attack Work");
        }
    }
}