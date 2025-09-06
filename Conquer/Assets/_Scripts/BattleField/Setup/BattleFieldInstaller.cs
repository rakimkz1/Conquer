using BattleField;
using ModestTree;
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
    [SerializeField] private ArmyStandRowHandler playerRowHandler;
    [SerializeField] private ArmyStandRowHandler enemyRowHandler;
    [SerializeField] private EnterToBattleInRowHandler playerRetreatPoint;
    [SerializeField] private EnterToBattleInRowHandler enemyRetreatPoint;

    public override void InstallBindings()
    {
        Container.Bind<BattleSceneSetting>().FromInstance(BattleSceneSetting).AsSingle();
        BindManagers();
        BindMVVM();
        BindScriptableObjects();
        BindPrefabs();
    }
    private void BindMVVM()
    {
        Container.Bind<UnitSelectPanel_Model>().AsSingle();
        Container.Bind<UnitSelectPanel_ViewModel>().AsSingle();
        Container.Bind<CommandPanel_ViewModel>().AsTransient();
        Container.Bind<CommandPanel_Model>().AsTransient();
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
        Container.Bind<EnemyWaveHandler>().AsSingle().NonLazy();
    }
    private void BindPrefabs()
    {
        Container.Bind<ArmyStandRowHandler>().WithId("playerArmyRow").FromInstance(playerRowHandler).AsCached();
        Container.Bind<ArmyStandRowHandler>().WithId("enemyArmyRow").FromInstance(enemyRowHandler).AsCached();
        Container.Bind<Transform>().WithId("playerRetreatPoint").FromInstance(playerRetreatPoint.gameObject.transform).AsCached();
        Container.Bind<Transform>().WithId("enemyRetreatPoint").FromInstance(enemyRetreatPoint.gameObject.transform).AsCached();
        Container.Bind<EnterToBattleInRowHandler>().WithId("playerEnterToBattleInRow").FromInstance(playerRetreatPoint).AsCached();
        Container.Bind<EnterToBattleInRowHandler>().WithId("enemyEnterToBattleInRow").FromInstance(enemyRetreatPoint).AsCached();
    }
    private void BindScriptableObjects()
    {
        Container.Bind<List<BattleMonsterPreset>>().FromInstance(so_monsterPresets).AsSingle();
    }
}
