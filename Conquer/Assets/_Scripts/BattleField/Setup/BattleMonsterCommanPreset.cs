using UnityEngine;

namespace BattleField
{
    [CreateAssetMenu(fileName = "BattleMonsterCommanPreset", menuName = "ScriptableObjects/Constants/MonsterCommanPreset")]
    public class BattleMonsterCommanPreset : ScriptableObject
    {
        public float ObstacleCheckDistance;
        public float ObstacleEvadingTime;
        public float ObstacleEvadeColdown;
        public float StopObstacleTime;
    }
}
