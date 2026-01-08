using UnityEngine;
using Zenject;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace BattleField
{
    public class ExtractorFactory
    {
        private GameObject _extractorPrefab;
        private GameObject _enemyExtractorPrefab;
        private DiContainer _container;
        private ResourceManager _resourceManager;
        private ExtractorSetUps _extractorSetup;
        private SaveManager _saveManager;
        private SaveData _saveData;
        [Inject(Id = "playerExtractorSpawnPoint")] private List<Transform> playerExtractorSpawnPoints;
        [Inject(Id = "enemyExtractorSpawnPoint")] private List<Transform> enemyExtractorSpawnPoints;
        private int _enemyExtactorsNumber;
        private int _playerExtractorsNumber;

        public ExtractorFactory(DiContainer container, ResourceManager resourceManager, SaveManager saveManager)
        {
            _container = container;
            _resourceManager = resourceManager;
            _saveManager = saveManager;
            _saveData = saveManager.Load();
            SetPrefab();
        }

        private void SetPrefab()
        {
            _resourceManager.LoadAsset<GameObject>("Assets/Prefabs/BattleField/Extractor.prefab", item =>
            {
                _extractorPrefab = item;
            });
            _resourceManager.LoadAsset<ExtractorSetUps>("Assets/Data/ScriptableObject/PlayerSetup/ExtractorSetup.asset", item =>
            {
                _extractorSetup = item;
            });
            _resourceManager.LoadAsset<GameObject>("Assets/Prefabs/BattleField/EnemyExtractor.prefab", asset =>
            {
                _enemyExtractorPrefab = asset;
            });
        }

        public async UniTask Create(bool isEnemy)
        {
            await UniTask.WaitWhile(() =>_extractorPrefab == null || _enemyExtractorPrefab == null);
            if (!isEnemy)
                CreateExtractor();
            else
                CreateEnemyExtractor();
        }


        private void CreateExtractor()
        {
            GameObject extractor = _container.InstantiatePrefab(_extractorPrefab);
            SetProperties(extractor.GetComponent<Extractor>());
        }
        private void CreateEnemyExtractor()
        {
            EnemyExtractor enemyExtractor = _container.InstantiatePrefab(_enemyExtractorPrefab).GetComponent<EnemyExtractor>();
            SetProperties(enemyExtractor);
        }

        private void SetProperties(EnemyExtractor enemyExtractor)
        {
            enemyExtractor.SetProperties(_extractorSetup.periodTime, _extractorSetup.manaPerPeriod, _extractorSetup.maxHealth, _extractorSetup.extractorAttackPrority);
            enemyExtractor.transform.position = enemyExtractorSpawnPoints[_enemyExtactorsNumber].position;
            enemyExtractor.targetPosition = enemyExtractor.transform;
            enemyExtractor.gameObject.GetComponent<EnemyExtractorView>().Init(enemyExtractor);
            _enemyExtactorsNumber++;
        }

        private void SetProperties(Extractor extractor)
        {
            extractor.SetProperties(_extractorSetup.manaPerPeriod, _extractorSetup.manaPerClick, _extractorSetup.periodTime, _extractorSetup.maxHealth, _extractorSetup.repairmentAmount, _extractorSetup.extractorAttackPrority);
            extractor.transform.position = playerExtractorSpawnPoints[_playerExtractorsNumber].position;
            extractor.targetPosition = extractor.transform;
            extractor.gameObject.GetComponent<ExtractorView>().Init(extractor);
            _playerExtractorsNumber++;
        }
    }
}
