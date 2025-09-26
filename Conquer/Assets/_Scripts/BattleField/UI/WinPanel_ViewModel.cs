using System;

namespace BattleField
{
    public class WinPanel_ViewModel : ViewModel<WinPanel_Model>
    {
        public event Action OnPlayerWin; 
        private GameOverHandler _gameOverHandler;
        public WinPanel_ViewModel(WinPanel_Model model, GameOverHandler gameOverHandler) : base(model)
        {
            _gameOverHandler = gameOverHandler;
            Init();
        }
        private void Init()
        {
            _gameOverHandler.OnWin += ShowWinPanel;
        }
        private void ShowWinPanel() => OnPlayerWin?.Invoke();

        public void Replay()
        {

        }

        public void ExitToRoadMap()
        {
            
        }
    }
}