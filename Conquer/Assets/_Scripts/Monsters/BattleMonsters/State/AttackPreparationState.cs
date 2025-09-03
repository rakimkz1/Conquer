using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Monsters
{
    public class AttackPreparationState : IBattleMonsterState
    {
        public bool IsReady = false;
        private CancellationTokenSource _cancel;
        public void OnEnter(BattleMonster target)
        {
            WaitAttackPreparation(target);
        }


        public void OnExit(BattleMonster target)
        {
            _cancel?.Cancel();
            Debug.Log("EndPreparation");
        }

        public void OnWork(BattleMonster target)
        {

        }
        private async UniTask WaitAttackPreparation(BattleMonster target)
        {
            _cancel = new CancellationTokenSource();
            IsReady = false;
            try
            {
                await UniTask.Delay((int)(target.attackPreparationTime * 1000f));
            }
            catch { return; }
            IsReady = true;
        }
    }
}