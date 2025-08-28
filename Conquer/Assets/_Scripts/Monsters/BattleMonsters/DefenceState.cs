using System;
using UnityEngine;

namespace Monsters
{
    public class DefenceState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            Debug.Log("Defence Enter");
        }

        public void OnExit(BattleMonster target)
        {
            Debug.Log("Defence Exit");
        }

        public void OnWork(BattleMonster target)
        {
            Debug.Log("Defence Work");
        }
    }
}