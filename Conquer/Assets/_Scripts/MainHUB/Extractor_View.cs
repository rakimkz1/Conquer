using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.MainHUB
{
    public class Extractor_View : View<Extractor_ViewModel>
    {
        [SerializeField] private Button btn_clickMainButton;
        [SerializeField] private TextMeshProUGUI txt_manaCounter;
        protected override void OnBind()
        {
            btn_clickMainButton.onClick.AddListener(ExtracterClicked);
            viewModel.OnManaChange += (value) =>
            {
                txt_manaCounter.text = value.ToString();
            };
        }

        private void ExtracterClicked()
        {
            viewModel.AddMana(viewModel.manaAmountOnClick.Value);
        }
    }
}