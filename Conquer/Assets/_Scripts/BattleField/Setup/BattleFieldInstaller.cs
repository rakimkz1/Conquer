using BattleField;
using System;
using UnityEngine;
using Zenject;

public class BattleFieldInstaller : MonoInstaller
{
    [SerializeField] private ResourceManager _resourceManager;
    public override void InstallBindings()
    {
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
    }
}
