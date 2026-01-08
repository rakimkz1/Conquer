using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectilePresets", menuName = "ScriptableObjects/Constants/ProjectilePresets")]
public class ProjectilesPresets : ScriptableObject
{
    
    public List<ProjectilePreset> presets = new List<ProjectilePreset>();

    public string GetPreset(MonsterIdelData data, bool isEnemy)
    {
        for(int i = 0; i < presets.Count; i++)
        {
            if (presets[i].data.monsterType == data.monsterType && isEnemy == presets[i].isEnemy && presets[i].data.monsterLevel == data.monsterLevel)
                return presets[i].presetPath;
        }
        throw new Exception($"Projectile preset{data.monsterType}  {data.monsterLevel} not exist");
    }

    [Serializable]
    public class ProjectilePreset
    {
        public MonsterIdelData data;
        public bool isEnemy;
        public string presetPath;
    }
}
