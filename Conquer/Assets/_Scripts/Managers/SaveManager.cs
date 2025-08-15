using System.IO;
using UnityEngine;
public class SaveManager
{
    private static string savePath = Application.persistentDataPath + "/save.json";

    public  void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"Data saved: {savePath}");
    }

    public SaveData Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        Debug.LogWarning("Data is not founded");
        return new SaveData();
    }

    public void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Data deleted");
        }
    }
}
