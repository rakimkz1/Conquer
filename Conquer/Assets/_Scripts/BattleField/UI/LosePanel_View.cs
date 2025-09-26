using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BattleField
{
    public class LosePanel_View : View<LosePanel_ViewModel>
    {
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private Button btn_peplayButton;
        [SerializeField] private Button btn_exitButton;
        protected override void OnBind() { }
        [Inject]
        private void Construct(LosePanel_ViewModel viewModel)
        {
            this.viewModel = viewModel;
            Init();
        }
        private void Init()
        {
            viewModel.OnLose += ShowLosePanel;
            btn_peplayButton.onClick.AddListener(() =>
            {
                viewModel.Replay();
            });
            btn_exitButton.onClick.AddListener(() =>
            {
                viewModel.ExitToRoadMap();
            });
        }

        private void ShowLosePanel()
        {
            _losePanel.SetActive(true);
        }
    }
}
