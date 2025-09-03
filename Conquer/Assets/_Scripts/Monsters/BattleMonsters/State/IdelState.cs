using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
namespace Monsters
{
    public class IdelState : IBattleMonsterState
    {
        private CancellationTokenSource _cancelToken;
        public void OnEnter(BattleMonster target)
        {
            TargetLoopCheck(target);
            target.RememberStayingPosition();
        }


        public void OnExit(BattleMonster target)
        {
            target.isMonsterInKeepingPosition = false;
            _cancelToken?.Cancel();
        }

        public void OnWork(BattleMonster target)
        {

        }
        private async UniTask TargetLoopCheck(BattleMonster target)
        {
            _cancelToken = new CancellationTokenSource();
            while (_cancelToken.IsCancellationRequested == false)
            {
                target.FindAttackTarget();
                await UniTask.Delay(1000);
            } 
        }
    }
}