using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace BattleField
{
    public class ExtractorFactory
    {
        private GameObject _extractorPrefab;
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
            _resourceManager.LoadAsset<GameObject>(_resourceManager.so_Keys.GetKey(PrefabKey.BattleFieldExtractor), item =>
            {
                _extractorPrefab = item;
            });
            _resourceManager.LoadAsset<ExtractorSetUps>(_resourceManager.so_Keys.GetKey(PrefabKey.ExtractorSetup), item =>
            {
                _extractorSetup = item;
            });
        }

        public async UniTask Create()
        {
            await UniTask.WaitWhile(() =>_extractorPrefab == null);
            if (_extractorPrefab == null)
                Debug.Log("extractor is null");
            GameObject target = _container.InstantiatePrefab(_extractorPrefab);
            Extractor  extractor = target.GetComponent<Extractor>();
            SetProperties(extractor);
        }

        private void SetProperties(Extractor extractor)
        {
            extractor.SetProperties(_extractorSetup.manaPerPeriod, _extractorSetup.manaPerClick, _extractorSetup.periodTime, _extractorSetup.maxHealth, _extractorSetup.repairmentAmount);
            extractor.transform.position = playerExtractorSpawnPoints[_playerExtractorsNumber].position;
            _playerExtractorsNumber++;
        }
    }
}
