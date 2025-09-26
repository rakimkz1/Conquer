using Monsters;
using System;
using UnityEngine;
using Zenject;

namespace BattleField
{
    public class GameOverHandler
    {
        private Base playerBase;
        private Base enemyBase;
        public event Action OnGameOver;
        public event Action OnWin;
        public event Action OnLose;
        public GameOverHandler([Inject(Id = "playerBase")] Base playerBase, [Inject(Id = "enemyBase")] Base enemyBase)
        {
            this.playerBase = playerBase;
            this.enemyBase = enemyBase;
            Init();
        }

        private void Init()
        {
            playerBase.OnDead += Lose;
            enemyBase.OnDead += Win;
        }

        private void Win(IAttackTarget target)
        {
            OnGameOver?.Invoke();
            OnWin?.Invoke();
        }

        private void Lose(IAttackTarget target)
        {
            OnGameOver?.Invoke();
            OnLose?.Invoke();
        }
    }
}