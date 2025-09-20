using BattleField;
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
    [SerializeField] private List<Transform> playerExtractorSpawnPoints;
    [SerializeField] private List<Transform> enemyExtractorSpawnPoints;


    public override void InstallBindings()
    {
        Container.Bind<BattleSceneSetting>().FromInstance(BattleSceneSetting).AsSingle();
        BindManagers();
        BindFactories();
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
        Container.Bind<UnitBuymentPanel_ModelView>().AsSingle();
        Container.Bind<UnitBuymentPanel_Model>().AsSingle();
    }
    private void BindManagers()
    {
        Container.Bind<SaveManager>().AsSingle();
        Container.Bind<ResourceManager>().FromInstance(_resourceManager).AsSingle();
        Container.Bind<DataTransferScene>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelBuilder>().AsSingle().NonLazy();
        Container.Bind<ArmyCommandHandler>().AsSingle();
        Container.Bind<AttackableUnitsOnSceneCollection>().AsSingle();
        Container.Bind<MonsterSpawnHandler>().AsSingle();
        Container.Bind<UnitsCommandKeeper>().AsSingle();
        Container.Bind<ManaHandler>().AsSingle();
        Container.Bind<EnemyManaHandler>().AsSingle();
        Container.Bind<BattleStarter>().AsSingle();
        Container.Bind<EnemyUnitBuymentHandler>().AsSingle();
        Container.Bind<AIPowerCanculator>().AsSingle();
        Container.Bind<EnemyCommandHandler>().AsSingle();
    }

    private void BindFactories()
    {
        Container.Bind<MonsterUnitFactory>().AsTransient().WithArguments(monsterUnitPrefab);
        Container.Bind<ExtractorFactory>().AsSingle().NonLazy();
        Container.Bind<EnemyAIFactory>().AsSingle().NonLazy();
    }

    private void BindPrefabs()
    {
        Container.Bind<ArmyStandRowHandler>().WithId("playerArmyRow").FromInstance(playerRowHandler).AsCached();
        Container.Bind<ArmyStandRowHandler>().WithId("enemyArmyRow").FromInstance(enemyRowHandler).AsCached();
        Container.Bind<Transform>().WithId("playerRetreatPoint").FromInstance(playerRetreatPoint.gameObject.transform).AsCached();
        Container.Bind<Transform>().WithId("enemyRetreatPoint").FromInstance(enemyRetreatPoint.gameObject.transform).AsCached();
        Container.Bind<EnterToBattleInRowHandler>().WithId("playerEnterToBattleInRow").FromInstance(playerRetreatPoint).AsCached();
        Container.Bind<EnterToBattleInRowHandler>().WithId("enemyEnterToBattleInRow").FromInstance(enemyRetreatPoint).AsCached();
        Container.Bind<List<Transform>>().WithId("playerExtractorSpawnPoint").FromInstance(playerExtractorSpawnPoints).AsCached();
        Container.Bind<List<Transform>>().WithId("enemyExtractorSpawnPoint").FromInstance(enemyExtractorSpawnPoints).AsCached();
    }
    private void BindScriptableObjects()
    {
        Container.Bind<List<BattleMonsterPreset>>().FromInstance(so_monsterPresets).AsSingle();
    }
}
