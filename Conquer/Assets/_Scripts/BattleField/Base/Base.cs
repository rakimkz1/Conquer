using Game_Setup;
using Monsters;
using ScriptableObjects;
using System;
using UnityEngine;
using Zenject;

namespace BattleField
{
    public class Base : MonoBehaviour, IAttackTarget
    {
        public float attackPriority { get; set; }
        public float powerScale { get; set; }
        public Transform targetPosition { get; set; }
        public bool isDead { get; set; }
        public bool isEnemy;

        [Header("Arhcers")]
        public int arhcerNumber;
        public int maxArcherInRow;
        public float distanceBetweenRows;
        public Vector2 archerAreaCenter;
        public Vector2 archerAreaScale;

        public BaseHealthHandler _healthHandler;
        public ArchersHandler _archerHandler;

        private LevelBuilder _levelBuilder;
        private AttackableCollection _targetCollection;
        private ResourceManager _resourceManager;
        private PlayerStartProperties _playerProperties;

        public event Action<IAttackTarget> OnDead;
        public event Action<IAttackTarget> OnExitTargetCollection;

        [SerializeField] private BaseArchersPresets so_ArhcerPresstt;

        [Inject]
        private void Construct(LevelBuilder levelBuilder, AttackableCollection targetCollection, ResourceManager resourceManager, ArchersHandler archersHandler)
        {
            _levelBuilder = levelBuilder;
            _targetCollection = targetCollection;
            _resourceManager = resourceManager;
            _archerHandler = archersHandler;
            LoadProperties();
        }
        private async void SetProperties()
        {
            targetPosition = transform;
            if (isEnemy)
                _targetCollection.AddEnemyUnit(this);
            else
                _targetCollection.AddPlayerUnit(this);

            _archerHandler.Init(arhcerNumber, maxArcherInRow, distanceBetweenRows, isEnemy, archerAreaCenter + (Vector2)transform.position, archerAreaScale);

            float maxHealth = (isEnemy) ? _levelBuilder.CurrentSceneSettings.enemyBaseHealth : _playerProperties.PlayerBaseHealth;
            _healthHandler = new BaseHealthHandler(maxHealth, maxHealth);
            attackPriority = 1f;
            _healthHandler.OnDestroy += OnBaseDestroy;
        }

        private void LoadProperties()
        {
            _resourceManager.LoadAsset<PlayerStartProperties>("Assets/Data/ScriptableObject/PlayerSetup/PlayerStartProperties.asset", asset =>
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(archerAreaCenter + (Vector2)transform.position, archerAreaScale);
            Gizmos.DrawSphere(transform.position, so_ArhcerPresstt.archerDistance);
            for(int i = 0; i < arhcerNumber; i++)
            {
                Gizmos.color = Color.green;
                Vector2 target = GetArcherPosition(i, arhcerNumber);
                Gizmos.DrawWireSphere(target + (Vector2)transform.position, 0.2f);
            }
        }

        private Vector3 GetArcherPosition(int archerID, int archersNumber)
        {
            Vector2 answer;
            answer.x = ((isEnemy) ? 1f : -1f) * (archerID / maxArcherInRow * distanceBetweenRows + ((archerAreaScale.x / maxArcherInRow) - archerAreaScale.x / 2)) +  archerAreaCenter.x;
            float betweenSpace = (archersNumber % maxArcherInRow != 0 && (archersNumber - 1) / maxArcherInRow == archerID / maxArcherInRow) ? archerAreaScale.y / ((archersNumber + 1) % maxArcherInRow) : archerAreaScale.y / maxArcherInRow;
            answer.y = (-archerAreaScale.y /2 + archerID % maxArcherInRow * betweenSpace + archerAreaCenter.y) + ((archersNumber % maxArcherInRow != 0 && (archersNumber - 1) / maxArcherInRow == archerID / maxArcherInRow) ? betweenSpace : 0f);
            return answer;
        }
    }
}