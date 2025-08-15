using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    Dictionary<string,string> data = new Dictionary<string,string>();
    public void Set<T>(string key, T value)
    {
        string jsonValue = JsonUtility.ToJson(new Wrapper<T>(value));
        data[key] = jsonValue;
    }

    public T Get<T> (string key, out bool isContain, T defaultValue = default)
    {
        if(data.TryGetValue(key, out string jsonValue))
        {
            isContain = true;
            return JsonUtility.FromJson<Wrapper<T>>(jsonValue).value;
        }
        isContain = false;
        return default;
    }


    [Serializable]
    public class Wrapper<T>
    {
        public T value;
        public Wrapper (T value) => this.value = value;
    } 
}
