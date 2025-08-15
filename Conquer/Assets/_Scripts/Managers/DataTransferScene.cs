using UnityEngine;
using System.Collections.Generic;
using System;

public class DataTransferScene : MonoBehaviour
{
    public Dictionary<string, object> data = new Dictionary<string, object>();

    public object Get(string key)
    {
        if(data.TryGetValue(key, out object obj))
        {
            return obj;
        }
        return default;
    }
    public void Set(string key, object value)
    {
        data[key] = value;
    }

    public void Remove(string key)
    {
        data.Remove(key);
    }
}
