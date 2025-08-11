using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainHUB.Extractor
{
    public class SpawnPanel_View : View<SpawnPanel_ViewModel>
    {
        [SerializeField] private SpawnPanel_Model _model;
        [SerializeField] private Button btn_buyButton;
        [SerializeField] private TextMeshProUGUI txt_costText;
        private IFactory<SpawnPanel_Model ,SpawnPanel_ViewModel> factory;

        [Inject]
        private void Constructor(IFactory<SpawnPanel_Model, SpawnPanel_ViewModel> factory)
        {
            this.factory = factory;
        }

        protected override void OnBind()
        {
            viewModel = factory.Create(_model);
            btn_buyButton.onClick.AddListener(OnBuyButtonPressed);
            txt_costText.text = viewModel.cost.ToString();
        }

        private void OnBuyButtonPressed()
        {
            bool isAccept = viewModel.SpawnMonster();
            if (isAccept)
                ShowBought();
            else
                ShowDontBought();
        }

        private void ShowBought()
        {
            Debug.Log("monster have bought");
        }

        private void ShowDontBought()
        {
            Debug.Log("monster haven't bought");
        }
    }
}