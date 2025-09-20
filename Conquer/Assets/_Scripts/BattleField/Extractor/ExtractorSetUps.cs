using UnityEngine;

namespace BattleField
{
    [CreateAssetMenu(fileName = "ExtractorSetup", menuName = "ScriptableObjects/Constants/ExtractorSetup")]
    public class ExtractorSetUps : ScriptableObject
    {
        public float manaPerPeriod;
        public float manaPerClick;
        public float periodTime;
        public float maxHealth;
        public float repairmentAmount;
        public float extractorAttackPrority;
    }
}
