using UnityEngine;

namespace Monsters
{
    public class AttackPreparationState : IBattleMonsterState
    {
        public bool IsReady = false;

        public void OnEnter(BattleMonster target)
        {
            Debug.Log("Attack Preparatoin Enter");
        }

        public void OnExit(BattleMonster target)
        {
            Debug.Log("Attack Preparation Exit");
        }

        public void OnWork(BattleMonster target)
        {
            Debug.Log("Attack Preparation Work");
        }
    }
}