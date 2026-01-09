using Cysharp.Threading.Tasks;
using Game_Setup;
using Monsters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace BattleField
{
    public class MonsterUnitFactory
    {
        private DiContainer _container;
        private GameObject _monsterPrefab;
        private List<BattleMonsterPreset> so_monsterPreset;
        private SaveManager _saveManager;
        private SaveData _saveData;
        private ResourceManager _resourceManager;
        private PlayerStartProperties so_playerStartProperties;
        private BattleMonsterPreset _preset;
        private BattleMonsterCommanPreset _commanPreset;
        private AttackableCollection _targetCollection;
        private ProjectileViewManager _projectileManager;
        private MonsterSpritesPreset so_spriteMonsterPreset;
        [Inject(Id = "playerArmyRow")] private ArmyStandRowHandler playerArmyRow;
        [Inject(Id = "enemyArmyRow")] private ArmyStandRowHandler enemyArmyRow;
        [Inject(Id = "playerRetreatPoint")] private Transform playerRetreatPoint;
        [Inject(Id = "enemyRetreatPoint")] private Transform enemyRetreatPoint;
        [Inject(Id = "playerEnterToBattleInRow")] private EnterToBattleInRowHandler playerEnterToBattle;
        [Inject(Id = "enemyEnterToBattleInRow")] private EnterToBattleInRowHandler enemyEnterToBattle;
        public event Action<BattleMonster> OnMonsterCreate;
        public MonsterUnitFactory(DiContainer container, GameObject monsterPrefab, List<BattleMonsterPreset> list, SaveManager saveManager, ResourceManager resourceManager, AttackableCollection targetCollection, MonsterSpritesPreset spritesPreset, ProjectileViewManager projectileManager)
        {
            _container = container;
            _targetCollection = targetCollection;
            _monsterPrefab = monsterPrefab;
            so_monsterPreset = list;
            _resourceManager = resourceManager;
            _projectileManager = projectileManager;
            so_spriteMonsterPreset = spritesPreset;
            _saveManager = saveManager;
            _saveData = _saveManager.Load();
            LoadResources();
        }

        public BattleMonster Create(bool isEnemy, MonsterIdelData type, ref Action OnSpawnEnd)
        {
            GameObject target = _container.InstantiatePrefab(_monsterPrefab);
            BattleMonster monster = target.GetComponent<BattleMonster>();
            if (isEnemy)
                target.transform.position = enemyRetreatPoint.position;
            else
                target.transform.position = playerRetreatPoint.position;
            _preset = FindMonsterPreset(type);
            monster.isEnemyUnit = isEnemy;
            monster.powerScale = _preset.powerScale;
            monster.isDead = false;
            SetMonsterSetting(type, monster);

            OnSpawnEnd += monster.Init;
            return monster;
        }
        public BattleMonster Spawn(BattleMonster target, bool isEnemy, MonsterIdelData type, ref Action OnSpawnEnd)
        {
            if (isEnemy)
                target.transform.position = enemyRetreatPoint.position;
            else
                target.transform.position = playerRetreatPoint.position;
            _preset = FindMonsterPreset(type);
            target.isEnemyUnit = isEnemy;
            target.powerScale = _preset.powerScale;
            target.isDead = false;
            target.gameObject.SetActive(true);
            SetMonsterSetting(type, target);

            OnSpawnEnd += target.Init;
            return target;
        }
        private void SetMonsterSetting(MonsterIdelData type, BattleMonster monster)
        {
            monster.monsterLevel = type.monsterLevel;
            monster.monsterType = type.monsterType;
            monster.targetPosition = monster.gameObject.transform;

            SetMonsterHealProperties(monster);
            SetAttackHandler(monster);
            SetDefenceHandler(monster);
            SetStandHandler(monster);
            SetRetreatHandler(monster);
            SetEvadeObstacleHandler(monster);
            AddToCollections(monster);
            SetSprite(monster, type);
            monster.healthHandler.maxHealth = _preset.maxHealth;
            monster.healthHandler.unitHealth.Value = _preset.maxHealth;
            OnMonsterCreate?.Invoke(monster);
        }

        private void SetMonsterHealProperties(BattleMonster monster)
        {
            float retreatHealAmount = _saveData.Get<float>(SaveDataKeys.PLAYER_RETREAT_HEAL_AMOUNT, out bool isContainHealAmount);
            float retreatHealColdown = _saveData.Get<float>(SaveDataKeys.PLAYER_RETREAT_HEAL_COLDOWN, out bool isContainHealColdown);
            if (!isContainHealAmount)
            {
                retreatHealAmount = so_playerStartProperties.RetreatHealAmount;
                _saveData.Set(SaveDataKeys.PLAYER_RETREAT_HEAL_AMOUNT, retreatHealAmount);
            }
            if (!isContainHealColdown)
            {
                retreatHealColdown = so_playerStartProperties.RetreatHealColdown;
                _saveData.Set(SaveDataKeys.PLAYER_RETREAT_HEAL_COLDOWN, retreatHealColdown);
            }
            monster.healthHandler = new HealthHandler();
            monster.healthHandler.retreadHealAmount = retreatHealAmount;
            monster.healthHandler.retreatHealColdown = retreatHealColdown;
        }
        private void SetAttackHandler(BattleMonster monster)
        {
            IMonsterAttackType attackType;
            if (monster.monsterType == MonsterType.Tanks)
                attackType = new AreaMeleeAttack(monster.isEnemyUnit, _preset.attackProperties.Damage, _preset.attackProperties.DamageAreaRadius, _targetCollection);
            else if (monster.monsterType == MonsterType.Sprinter)
                attackType = new TargetMeleeAttack(monster.isEnemyUnit, _preset.attackProperties.Damage);
            else if (monster.monsterType == MonsterType.Rangers)
                attackType = new TargetRangeAttack(monster.isEnemyUnit, _preset.attackProperties.Damage, _preset.attackProperties.MissileSpeed, _projectileManager);
            else if (monster.monsterType == MonsterType.Mage)
                attackType = new AreaRangeAttack(monster.isEnemyUnit, _preset.attackProperties.Damage, _preset.attackProperties.DamageAreaRadius, _preset.attackProperties.MissileSpeed, _targetCollection, _projectileManager);
            else
                attackType = new TargetRangeAttack(monster.isEnemyUnit, _preset.attackProperties.Damage, _preset.attackProperties.MissileSpeed, _projectileManager);
            monster.attackHandler = new AttackHandler(monster, attackType, _preset.attackSpeed, _preset.attackDistance, _preset.attackPreparationTime, _preset.speed);
        }
        private void SetDefenceHandler(BattleMonster monster)
        {
            ArmyStandRowHandler rowHandler = monster.isEnemyUnit ? enemyArmyRow : playerArmyRow;
            monster.defenceHandler = new DefenceHander(monster, rowHandler, _preset.speed, _preset.defenceTraceDistance, _preset.defenceProvocationDistance);
        }
        private void SetStandHandler(BattleMonster monster)
        {
            monster.standPositionHandler = new StandPositionHandler(monster, _preset.speed, _preset.provocationDistance, _preset.maxTracingDistance);
        }
        private void SetRetreatHandler(BattleMonster monster)
        {
            EnterToBattleInRowHandler enterBattle = monster.isEnemyUnit ? enemyEnterToBattle : playerEnterToBattle;
            Transform pointPos = monster.isEnemyUnit ? enemyRetreatPoint : playerRetreatPoint;
            monster.retreatHandler = new RetreatHandler(monster, enterBattle, pointPos, _preset.speed);
        }
        private void SetEvadeObstacleHandler(BattleMonster monster)
        {
            EvadeObstacalseHanlder obstacleHandler = new EvadeObstacalseHanlder(_preset.speed, _commanPreset.ObstacleCheckDistance, monster, _commanPreset.ObstacleEvadingTime, _commanPreset.ObstacleEvadeColdown, _commanPreset.StopObstacleTime);
            monster.evadeHandler = obstacleHandler;
        }
        private void AddToCollections(BattleMonster monster)
        {
            if (monster.isEnemyUnit)
                enemyEnterToBattle.Add(monster);
            else
                playerEnterToBattle.Add(monster);
        }
        private async Task SetSprite(BattleMonster monster, MonsterIdelData type)
        {
            _resourceManager.LoadAsset<GameObject>(so_spriteMonsterPreset.GetSprite(monster.isEnemyUnit, type), asset =>
            {
                GameObject target = UnityEngine.Object.Instantiate(asset, monster.transform.position, asset.transform.rotation, monster.transform);
                monster.viewMonster.animationManager = new BattleMonsterAnimationManager(target.GetComponentInChildren<Animator>());
                monster.spriteContainer = target.GetComponent<BattleMonsterSpriteContainer>();
            });
        }
        private async UniTask LoadResources()
        {
            _resourceManager.LoadAsset<PlayerStartProperties>("Assets/Data/ScriptableObject/PlayerSetup/PlayerStartProperties.asset", value =>
            {
                so_playerStartProperties = value;
            });
            _resourceManager.LoadAsset<BattleMonsterCommanPreset>("Assets/Data/ScriptableObject/PlayerSetup/BattleMonsterCommanPreset.asset", item =>
            {
                _commanPreset = item;
            });
        }

        private BattleMonsterPreset FindMonsterPreset(MonsterIdelData type)
        {
            for(int i = 0; i < so_monsterPreset.Count; i++)
            {
                if (Equals(so_monsterPreset[i].data, type))
                    return so_monsterPreset[i];
            }
            return null;
        }

    }
}