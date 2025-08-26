using BattleField;
using UnityEngine;
using Zenject;

public class BattleFieldInstaller : MonoInstaller
{
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private BattleSceneSetting BattleSceneSetting;
    public override void InstallBindings()
    {
        Container.Bind<BattleSceneSetting>().AsSingle();
        BindManagers();
        BindMVVM();
    }

    private void BindMVVM()
    {
        Container.Bind<UnitSelectPanel_Model>().AsTransient();
        Container.Bind<UnitSelectPanel_ViewModel>().AsTransient();
    }

    private void BindManagers()
    {
        Container.Bind<SaveManager>().AsSingle();
        Container.Bind<ResourceManager>().FromInstance(_resourceManager).AsSingle();
        Container.Bind<DataTransferScene>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelBuilder>().AsSingle().NonLazy();
    }
}
