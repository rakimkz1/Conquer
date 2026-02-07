using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "IdelMosterSpritePreset", menuName = "ScriptableObjects/Constants/IdelMonsterSpritePreset")]
public class IdelMonsterSpritePreset : ScriptableObject
{
    public List<IdelMonsterSprite> idelMonsterSpritePresetPath;

    public string GetSprite(MonsterIdelData data)
    {
        for (int i = 0; i < idelMonsterSpritePresetPath.Count; i++)
        {
            if(Equals(data, idelMonsterSpritePresetPath[i].data))
            {
                return idelMonsterSpritePresetPath[i].spritePath;
            }
        }
        return null;
    }

    [Serializable]
    public class IdelMonsterSprite
    {
        public string spritePath;
        public MonsterIdelData data;
    }
}
