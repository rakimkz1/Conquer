using UnityEngine;

namespace BattleField
{
    [CreateAssetMenu(fileName = "BattleMonsterPreset", menuName = "ScriptableObjects/Monster/BattleMonsterPreset")]
    public class BattleMonsterPreset : ScriptableObject
    {
        public MonsterIdelData data;
        public float provocationDistance;
        public float maxTracingDistance;
        public float speed;
        public float attackDistance;
        public float attackSpeed;
        public float attackPreparationTime;
        public float attackPriority;
        public float maxHealth;
    }
}