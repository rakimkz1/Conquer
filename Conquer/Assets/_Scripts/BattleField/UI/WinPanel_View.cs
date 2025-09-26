using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BattleField
{
    public class WinPanel_View : View<WinPanel_ViewModel>
    {

        [SerializeField] private GameObject _winPanel;
        [SerializeField] private Button btn_replay;
        [SerializeField] private Button btn_exitToRoadMap;
        protected override void OnBind() { }
        [Inject]
        private void Construct(WinPanel_ViewModel viewModel)
        {
            this.viewModel = viewModel;
            Init();
        }
        private void Init()
        {
            viewModel.OnPlayerWin += ShowWinPanel;
            btn_replay.onClick.AddListener(() =>
            {
                viewModel.Replay();
            });
            btn_exitToRoadMap.onClick.AddListener(() =>
            {
                viewModel.ExitToRoadMap();
            });
        }

        private void ShowWinPanel()
        {
            _winPanel.SetActive(true);
        }
    }
}