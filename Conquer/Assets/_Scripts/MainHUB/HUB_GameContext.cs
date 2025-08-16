using MainHUB.Extractor;
using Assets._Scripts.Managers;
using UnityEngine;
using Zenject;
using MainHUB.HUB_Managers;
using System;

public class HUB_GameContext : MonoInstaller
{
    [SerializeField] private ManaManager manaManager;
    [SerializeField] private MonsterSpawnManager monsterSpawnManager;
    [SerializeField] private GameObject resourceManager;
    [SerializeField] private GameObject audioManager;
    public override void InstallBindings()
    {
        BindManagers();
        BindFactory();
    }
    private void BindManagers()
    {
        Container.Bind<MonsterSpawnManager>().FromInstance(monsterSpawnManager).AsSingle();
        Container.Bind<MonsterCollection>().AsSingle();

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
}