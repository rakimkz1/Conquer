using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BattleField
{

    public class UnitSelectPanel_View : View<UnitSelectPanel_ViewModel>
    {
        [SerializeField] private GameObject _unselectedPanel;
        [SerializeField] private GameObject _selectedPanel;
        [SerializeField] private Button _startBattleButton;
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
            viewModel.OnShowStartBattleButton += ShowStartBattleButton;
            viewModel.OnHideStartBatlleButton += HideStartBattleButton;
            viewModel.HideWholeSelectionPanel += HideWholeSelectionPanel;
            _startBattleButton.onClick.AddListener(() =>
            {
                viewModel.PressStartBattleButton();
            });
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
            string key = "Assets/Prefabs/BattleField/SelectionIcon.prefab";
            _resourceManager.LoadAsset<GameObject>(key, prefab =>
            {
                _monsterIconPrefab = prefab;
            });
            await UniTask.WaitUntil(() => _monsterIconPrefab != null);
        }

        private void InitializeIcon(MonsterIdelData monsterIdelData)
        {
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
        private void HideWholeSelectionPanel()
        {
            gameObject.SetActive(false);
        }

        public void ShowStartBattleButton() => _startBattleButton.gameObject.SetActive(true);

        public void HideStartBattleButton() => _startBattleButton.gameObject.SetActive(false);
    }
}
