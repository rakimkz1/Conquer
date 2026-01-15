
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BaseArcherPreset", menuName = "ScriptableObjects/Constants/BaseArcherPreset")]
    public class BaseArchersPresets : ScriptableObject
    {
        public float arrowDamage;
        public float archerDistance;
        public float attackingSpeed;
        public float projectileSpeed;
    }
}
