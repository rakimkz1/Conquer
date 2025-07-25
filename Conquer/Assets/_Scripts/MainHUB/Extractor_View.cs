using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.MainHUB
{
    public class Extractor_View : View<Extractor_ViewModel>
    {
        [SerializeField] private GameObject obj_manaExtractorPanel;
        [SerializeField] private Transform tr_manaExtractorPanelStarterPoint;
        [SerializeField] private Transform tr_manaExtractorEndPoint;

        [SerializeField] private Button btn_clickMainButton;
        [SerializeField] private Button btn_rollUpPanel;
        [SerializeField] private TextMeshProUGUI txt_manaCounter;
        protected override void OnBind()
        {
            btn_clickMainButton.onClick.AddListener(ExtracterClicked);
            btn_rollUpPanel.onClick.AddListener(RollUpPanel);
            viewModel.OnManaChange += (value) => txt_manaCounter.text = value.ToString();
        }

        private void ExtracterClicked()
        {
            viewModel.AddMana(viewModel.manaAmountOnClick.Value);
        }

        private void RollUpPanel()
        {
            if (viewModel.isPanelRolledUp)
            {
                obj_manaExtractorPanel.transform.DOMove(tr_manaExtractorPanelStarterPoint.position, 0.8f).SetEase(Ease.OutExpo);
                viewModel.isPanelRolledUp = false;
            }
            else
            {
                obj_manaExtractorPanel.transform.DOMove(tr_manaExtractorEndPoint.position, 0.8f).SetEase (Ease.OutExpo);
                viewModel.isPanelRolledUp = true;
            }
        }
    }
}