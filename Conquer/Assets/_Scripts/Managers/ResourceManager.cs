using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<string, AsyncOperationHandle> loadedAssets = new Dictionary<string, AsyncOperationHandle>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Загрузка ресурса (например, префаба)
    public void LoadAsset<T>(string key, Action<T> onLoaded) where T : UnityEngine.Object
    {
        if (loadedAssets.ContainsKey(key))
        {
            onLoaded?.Invoke(loadedAssets[key].Result as T);
            return;
        }

        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
        handle.Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                loadedAssets[key] = op;
                onLoaded?.Invoke(op.Result);
            }
            else
            {
                Debug.LogError($"Failed to load asset with key: {key}");
            }
        };
    }

    // Освобождение ресурса
    public async UniTask ReleaseAsset(string key)
    {
        if (loadedAssets.TryGetValue(key, out var handle))
        {
            Addressables.Release(handle);
            await handle.ToUniTask();
            loadedAssets.Remove(key);
        }
    }

    // Асинхронная загрузка сцены
    public async UniTask LoadScene(string sceneKey)
    {
        var handle = Addressables.LoadSceneAsync(sceneKey);
        await handle.Task;
    }

    // Instantiate префаба по ключу
    public async UniTask<GameObject> InstantiateAsync(string key, Vector3 position, Quaternion rotation, Action<GameObject> onInstantiated = null)
    {
        var handle = Addressables.InstantiateAsync(key, position, rotation);
        var instance = await handle.Task;
        onInstantiated?.Invoke(instance);
        return instance;
    }
}
