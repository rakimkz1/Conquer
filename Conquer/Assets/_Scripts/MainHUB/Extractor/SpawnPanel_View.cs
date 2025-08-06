using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainHUB.Extractor
{
    public class SpawnPanel_View : View<SpawnPanel_ViewModel>
    {

        [SerializeField] private Button btn_buyButton;
        [SerializeField] private TextMeshProUGUI txt_costText;
        private SpawnPanel_ViewModel.Factory factory;

        [Inject]
        private void Constructor(SpawnPanel_ViewModel.Factory factory)
        {
            this.factory = factory;
        }

        protected override void OnBind()
        {
            viewModel = factory.Create();
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