using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSpritePreset", menuName = "ScriptableObjects/Constants/MonsterSpritePreset")]
public class MonsterSpritesPreset : ScriptableObject
{
    public List<MonsterSprite> monsterSprites = new List<MonsterSprite>();

    public string GetSprite(bool isEnemy, MonsterIdelData data)
    {
        for(int i = 0; i < monsterSprites.Count; i++)
        {
            if (monsterSprites[i].isEnemy == isEnemy && monsterSprites[i].data.monsterType == data.monsterType && monsterSprites[i].data.monsterLevel == data.monsterLevel)
            {
                return monsterSprites[i].path;
            }
        }
        return null;
    }
}
[Serializable]
public class MonsterSprite
{
    public string path;
    public bool isEnemy;
    public MonsterIdelData data;
}
