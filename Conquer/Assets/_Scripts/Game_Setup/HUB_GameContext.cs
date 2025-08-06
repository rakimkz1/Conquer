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

    public override void InstallBindings()
    {
        BindManagers();
        BindModels();
        BindFactory();
    }
    private void BindManagers()
    {
        Container.Bind<ManaManager>().FromInstance(manaManager).AsSingle();
        Container.Bind<MonsterSpawnManager>().FromInstance(monsterSpawnManager).AsSingle();
    }
    private void BindFactory()
    {
        Container.Bind<Extractor_ViewModel.Factory>().AsTransient();
        Container.Bind<SpawnPanel_ViewModel.Factory>().AsTransient();
    }
    private void BindModels()
    {
        Container.Bind<Extractor_Model>().AsCached();
        Container.Bind<SpawnPanel_Model>().AsCached();
    }
}