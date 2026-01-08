using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject ResourceManager;
    [SerializeField] private GameObject AudioManager;
    private ResourceManager resource;
    private string currentSceneKey;
    private void Awake()
    { 
        LoadAllManagers();
    }

    private async UniTaskVoid LoadAllManagers()
    {
        resource = Instantiate(ResourceManager).GetComponent<ResourceManager>();
        var audio = Instantiate(AudioManager);
        audio.GetComponent<AudioManager>().Initialize();

        await resource.InstantiateAsync("Assets/Prefabs/UI/LoadingSceen.prefab", transform.position, Quaternion.identity);

        await resource.LoadScene(SceneKey.MAIN_MENU);
        currentSceneKey = SceneKey.MAIN_MENU;
    }
    public async UniTask LoadScene(string key)
    {
        await resource.InstantiateAsync("Assets/Prefabs/UI/LoadingSceen.prefab", transform.position, Quaternion.identity);
        await resource.ReleaseAsset(currentSceneKey);
        await resource.LoadScene(key);
        await resource.ReleaseAsset("Assets/Prefabs/UI/LoadingSceen.prefab");
        currentSceneKey = key;
    }

}