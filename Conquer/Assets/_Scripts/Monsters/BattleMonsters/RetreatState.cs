using UnityEngine;

namespace Monsters
{
    public class RetreatState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            Debug.Log("Retreat Enter");
        }

        public void OnExit(BattleMonster target)
        {
            Debug.Log("Retreat Exit");
        }

        public void OnWork(BattleMonster target)
        {
            Debug.Log("Retreat Work");
        }
    }
}