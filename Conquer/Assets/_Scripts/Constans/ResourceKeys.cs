using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceKeys", menuName = "ScriptableObjects/Constants/ResourceKey")]
public class ResourceKeys : ScriptableObject
{
    public List<ResourceData> keys = new List<ResourceData>();
      
    public string GetKey(PrefabKey key)
    {
        return keys[(int)key].prefabLocation;
    }
}
[Serializable]
public class ResourceData
{
    public PrefabKey key;
    public string prefabLocation;
}
