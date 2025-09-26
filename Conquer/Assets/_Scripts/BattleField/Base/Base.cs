using Game_Setup;
using Monsters;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace BattleField
{
    public class Base : MonoBehaviour, IAttackTarget
    {
        public float attackPriority { get; set; }
        public float powerScale { get; set; }
        public Vector3 targetPosition { get; set; }
        public bool isDead { get; set; }
        public bool isEnemy;

        private BaseHealthHandler _healthHandler;

        private LevelBuilder _levelBuilder;
        private AttackableCollection _targetCollection;
        private ResourceManager _resourceManager;
        private PlayerStartProperties _playerProperties;

        public event Action<IAttackTarget> OnDead;
        public event Action<IAttackTarget> OnExitTargetCollection;

        [Inject]
        private void Construct(LevelBuilder levelBuilder, AttackableCollection targetCollection, ResourceManager resourceManager)
        {
            _levelBuilder = levelBuilder;
            _targetCollection = targetCollection;
            _resourceManager = resourceManager;
            LoadProperties();
        }
        private async void SetProperties()
        {
            targetPosition = transform.position;
            if (isEnemy)
                _targetCollection.AddEnemyUnit(this);
            else
                _targetCollection.AddPlayerUnit(this);

            float maxHealth = (isEnemy) ? _levelBuilder.CurrentSceneSettings.enemyBaseHealth : _playerProperties.PlayerBaseHealth;
            _healthHandler = new BaseHealthHandler(maxHealth, maxHealth);
            _healthHandler.OnDestroy += OnBaseDestroy;
        }


        private void LoadProperties()
        {
            _resourceManager.LoadAsset<PlayerStartProperties>(_resourceManager.so_Keys.GetKey(PrefabKey.PlayerStartProperties), asset =>
            {
                _playerProperties = asset;
                SetProperties();
            });
        }

        public void TakeDamage(float damage) => _healthHandler.TakeDamage(damage);
        private void OnBaseDestroy()
        {
            OnDead?.Invoke(this);
        }
    }
}