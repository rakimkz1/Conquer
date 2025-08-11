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
    [SerializeField] GameObject[] instancesForInject;

    public override void InstallBindings()
    {
        BindManagers();
        //BindModels();
        BindFactory();
        BindInstances();
    }

    private void BindManagers()
    {
        //Debug.Log(manaManager.GetComponent<ManaManager>() == null);
        //Container.Bind<ManaManager>().FromInstance(manaManager);
        Container.Bind<MonsterSpawnManager>().FromInstance(monsterSpawnManager).AsSingle();
    }
    private void BindFactory()
    {
        Container.Bind<IFactory<Extractor_Model, Extractor_ViewModel>>().To<Extractor_ViewModel.Factory>().AsTransient();
        Container.Bind<IFactory<SpawnPanel_Model, SpawnPanel_ViewModel>>().To<SpawnPanel_ViewModel.Factory>().AsTransient();
    }
    private void BindInstances()
    {
        foreach (GameObject instance in instancesForInject)
        {
            Container.Inject(instance);
        }
    }
    private void BindModels()
    {
        Container.Bind<Extractor_Model>().AsCached();
        Container.Bind<SpawnPanel_Model>().AsCached();
    }
}