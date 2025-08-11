using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainHUB.Extractor
{
    public class Extractor_View : View<Extractor_ViewModel>
    {
        #region Properties

        public Extractor_Model _model;
        [SerializeField] private GameObject obj_manaExtractorPanel;
        [SerializeField] private Transform tr_manaExtractorPanelStarterPoint;
        [SerializeField] private Transform tr_manaExtractorEndPoint;

        [SerializeField] private Button btn_clickMainButton;
        [SerializeField] private Button btn_rollUpPanel;
        [SerializeField] private TextMeshProUGUI txt_manaCounter;
        private IFactory<Extractor_Model, Extractor_ViewModel> _factory;
        #endregion

        #region Init

        [Inject]
        private void Construct(IFactory<Extractor_Model, Extractor_ViewModel> factory)
        {
            _factory = factory;
        }
        protected override void OnBind()
        {
            viewModel = _factory.Create(_model);
            btn_clickMainButton.onClick.AddListener(ExtracterClicked);
            btn_rollUpPanel.onClick.AddListener(RollUpPanel);
            viewModel.OnManaChange += (value) => txt_manaCounter.text = value.ToString();
        }
        #endregion

        private void ExtracterClicked()
        {
            viewModel.AddMana(viewModel.manaAmountOnClick.Value);
        }

        private void RollUpPanel() // Roll up Extractor and Spawn Panel
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