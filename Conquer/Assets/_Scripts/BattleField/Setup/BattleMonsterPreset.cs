using System;
using UnityEngine;

namespace BattleField
{
    [CreateAssetMenu(fileName = "BattleMonsterPreset", menuName = "ScriptableObjects/Monster/BattleMonsterPreset")]
    public class BattleMonsterPreset : ScriptableObject
    {
        public MonsterIdelData data;
        public float provocationDistance;
        public float maxTracingDistance;
        public float defenceProvocationDistance;
        public float defenceTraceDistance;
        public float speed;
        public float powerScale;
        public float attackDistance;
        public float attackSpeed;
        public float attackPreparationTime;
        public float attackPriority;
        public float maxHealth;
        public AttackProperties attackProperties;

        [Serializable] 
        public class AttackProperties
        {
            public float Damage;
            public float MissileSpeed;
            public float DamageAreaRadius;
        }
    }
}