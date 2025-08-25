using UnityEngine;

namespace BattleField
{
    [CreateAssetMenu(fileName = "BattleFieldSceneSetting", menuName = "ScriptableObjects/BattleField/SceneSetting")]
    public class BattleSceneSetting : ScriptableObject 
    {
        public int allowedMonstersNumber;
    }
}
