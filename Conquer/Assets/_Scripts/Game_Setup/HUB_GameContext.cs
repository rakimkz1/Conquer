using MainHUB.Extractor;
using Assets._Scripts.Managers;
using UnityEngine;
using Zenject;
using MainHUB.HUB_Managers;

public class HUB_GameContext : MonoInstaller
{
    [SerializeField] private ManaManager manaManager;
    [SerializeField] private MonsterSpawnManager monsterSpawnManager;

    public override void InstallBindings()
    {
        BindManagers();
        BindFactory();
    }

    private void BindManagers()
    {
        Container.Bind<MonsterSpawnManager>().FromInstance(monsterSpawnManager).AsSingle();
    }
    private void BindFactory()
    {
        Container.Bind<IFactory<Extractor_Model, Extractor_ViewModel>>().To<Extractor_ViewModel.Factory>().AsTransient();
        Container.Bind<IFactory<SpawnPanel_Model, SpawnPanel_ViewModel>>().To<SpawnPanel_ViewModel.Factory>().AsTransient();
    }
}