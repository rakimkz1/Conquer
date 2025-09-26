using System.Collections;
using UnityEngine;

namespace Game_Setup
{
    [CreateAssetMenu(fileName = "PlayerStartProperties", menuName = "ScriptableObjects/Constants/PlayerStartProperties")]
    public class PlayerStartProperties : ScriptableObject
    {
        public float RetreatHealColdown;
        public float RetreatHealAmount;
        public int PlayerExtractorNumber;
        public float PlayerBaseHealth;
    }
}