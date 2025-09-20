using System;
using UnityEngine;

namespace BattleField
{
    [Serializable]
    public class EnemyAIProperties
    {
        [Range(0f, 1f)] public float armyProportionToDefence;
        [Range(0f, 2f)] public float armyProportionsToAttack;
        [Range(0f, 1f)] public float armyProportionToRetreat;
        public float stateSwitchColdown;
        public float playerCommandListenTime;
    }
}