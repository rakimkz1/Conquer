using MainHUB.Extractor;
using Assets._Scripts.Managers;
using UnityEngine;
using Zenject;
using MainHUB.HUB_Managers;

public class HUB_GameContext : MonoInstaller
{
    [SerializeField] private ManaManager manaManager;
    [SerializeField] private MonsterSpawnManager monsterSpawnManager;
    [SerializeField] private GameObject resourceManager;
    [SerializeField] private GameObject audioManager;
    [SerializeField] private IdelMonsterEggSpritePresets so_EggSpritePreset;
    [SerializeField] private IdelMonsterSpritePreset so_IdelMonsterSpritePreset;
    public override void InstallBindings()
    {
        BindManagers();
        BindFactory();
        BindScriptableObject();
    }
    private void BindManagers()
    {
        Container.Bind<MonsterSpawnManager>().FromInstance(monsterSpawnManager).AsSingle();
        Container.Bind<MonsterCollection>().AsSingle();
        Container.Bind<SaveManager>().AsSingle();

        GameObject resource = Container.InstantiatePrefab(resourceManager);
        GameObject audio = Container.InstantiatePrefab(audioManager);
        Container.Bind<ResourceManager>().FromInstance(resource.GetComponent<ResourceManager>()).AsSingle().NonLazy();
        Container.Bind<AudioManager>().FromInstance(audio.GetComponent<AudioManager>()).AsSingle().NonLazy();
    }
    private void BindFactory()
    {
        Container.Bind<IFactory<Extractor_Model, Extractor_ViewModel>>().To<Extractor_ViewModel.Factory>().AsTransient();
        Container.Bind<IFactory<SpawnPanel_Model, SpawnPanel_ViewModel>>().To<SpawnPanel_ViewModel.Factory>().AsTransient();
    }

    private void BindScriptableObject()
    {
        Container.Bind<IdelMonsterSpritePreset>().FromInstance(so_IdelMonsterSpritePreset).AsSingle();
        Container.Bind<IdelMonsterEggSpritePresets>().FromInstance(so_EggSpritePreset).AsSingle();
    }
}