using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace BattleField
{

    public class UnitSelectPanel_View : View<UnitSelectPanel_ViewModel>
    {
        [SerializeField] private GameObject _unselectedPanel;
        [SerializeField] private GameObject _selectedPanel;
        private ResourceManager _resourceManager;
        private SaveManager _saveManager;
        private SaveData _saveData;
        private GameObject _monsterIconPrefab;
        [Inject]
        private void Construct(UnitSelectPanel_ViewModel viewModel, ResourceManager resource, SaveManager save)
        {
            this.viewModel = viewModel;
            _resourceManager = resource;
            _saveManager = save;
            _saveData = _saveManager.Load();
            SpawnAllMonsterIcon();
        }

        protected override void OnBind()
        {
            
        }

        private async void SpawnAllMonsterIcon()
        {
            List<MonsterIdelData> list = _saveData.Get<List<MonsterIdelData>>(SaveDataKeys.MONSTER_IDEL_DATA_LIST, out bool isContain);
            if (!isContain)
                return;

            await LoadAllPrefabs();

            for(int i = 0; i < list.Count; i++)
            {
                InitializeIcon(list[i]);
            }
        }

        private async UniTask LoadAllPrefabs()
        {
            string key = _resourceManager.so_Keys.GetKey(PrefabKey.MonsterSelectionIcon);
            _resourceManager.LoadAsset<GameObject>(key, prefab =>
            {
                _monsterIconPrefab = prefab;
                Debug.Log("monsterprefab seted");
            });
            await UniTask.WaitUntil(() => _monsterIconPrefab != null);
        }

        private void InitializeIcon(MonsterIdelData monsterIdelData)
        {
            if(_monsterIconPrefab == null)
            {
                Debug.Log("_monsterIcon is null");
            }
            GameObject target = Instantiate(_monsterIconPrefab);
            target.transform.SetParent(_unselectedPanel.transform);
            MonsterSelectionIcon icon = target.GetComponent<MonsterSelectionIcon>();
            icon.Init(monsterIdelData);
            icon.OnPressed += ChangeIconStatus;
        }

        private void ChangeIconStatus(MonsterSelectionIcon icon)
        {
            if (icon.isSelected)
            {
                icon.transform.SetParent(_selectedPanel.transform);
                viewModel.selectedMonsters.Add(icon.MonsterType);
                viewModel.notSelectedMonsters.Remove(icon.MonsterType);
            }
            else
            {
                icon.transform.SetParent(_unselectedPanel.transform);
                viewModel.notSelectedMonsters.Add(icon.MonsterType);
                viewModel.selectedMonsters.Remove(icon.MonsterType);
            }
        }
    }
}
