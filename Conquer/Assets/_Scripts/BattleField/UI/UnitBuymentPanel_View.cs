using System;
using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BattleField
{
    public class UnitBuymentPanel_View : View<UnitBuymentPanel_ModelView>
    {
        private ReactiveProperty<int> unitCost = new ReactiveProperty<int>();
        [SerializeField]private TextMeshProUGUI txt_unitCost;
        [SerializeField] private TextMeshProUGUI txt_manaAmount;
        [SerializeField] private Button btn_OnBuyUnit;
        [Inject]
        private void Construct(UnitBuymentPanel_ModelView viewModel, UnitSelectPanel_ViewModel selectionViewModel, BattleStarter battleStarter)
        {
            battleStarter.OnBattleStart += ShowPanel;
            this.viewModel = viewModel;
            Init();
        }

        private void Init()
        {
            viewModel.unitCost.Subscribe(value => unitCost.Value = value);
            viewModel.manaAmount.Subscribe(value => ShowManaAmount(value));
            unitCost.Subscribe(value => ShowUnitCost(value));
            btn_OnBuyUnit.onClick.AddListener(() =>
            {
                viewModel.OnBuyUnitPress();
            });
        }

        protected override void OnBind() {}

        private void ShowPanel()
        {
            gameObject.SetActive(true);
        }

        private void ShowManaAmount(float value)
        {
            txt_manaAmount.text = ((int)value).ToString();
        }

        public void ShowUnitCost(int value)
        {
            txt_unitCost.text = value.ToString();
        }
    }
}