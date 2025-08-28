using BattleField;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleFieldInstaller : MonoInstaller
{
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private BattleSceneSetting BattleSceneSetting;
    [SerializeField] private List<BattleMonsterPreset> so_monsterPresets;
    [SerializeField] private GameObject monsterUnitPrefab;
    public override void InstallBindings()
    {
        Container.Bind<BattleSceneSetting>().AsSingle();
        BindManagers();
        BindMVVM();
        BindScriptableObjects();
        BindPrefabs();
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
        Container.Bind<ArmyCommandHandler>().AsSingle();
        Container.Bind<AttackableUnitsOnSceneCollection>().AsSingle();
        Container.Bind<MonsterUnitFactory>().AsTransient().WithArguments(monsterUnitPrefab);
    }
    private void BindPrefabs()
    {
        
    }
    private void BindScriptableObjects()
    {
        Container.Bind<List<BattleMonsterPreset>>().FromInstance(so_monsterPresets).AsSingle();
    }
}
