using System;

namespace BattleField
{
    public class LosePanel_ViewModel : ViewModel<LosePanel_Model>
    {
        private GameOverHandler _gameOverHandler;

        public event Action OnLose;
        public LosePanel_ViewModel(LosePanel_Model model, GameOverHandler gameOverHandler) : base(model)
        {
            _gameOverHandler = gameOverHandler;
            Init();
        }
        private void Init()
        {
            _gameOverHandler.OnLose += () => OnLose?.Invoke();
        }

        public void Replay()
        {
        }

        public void ExitToRoadMap()
        {
        }
    }
}
