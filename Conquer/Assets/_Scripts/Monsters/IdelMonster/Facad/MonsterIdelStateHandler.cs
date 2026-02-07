using Cysharp.Threading.Tasks;
using Monsters.MonsterState;
using System.Threading;
using UnityEngine;

namespace Monsters
{
    public class MonsterIdelStateHandler
    {
        public IdelStateBase currentState;
        public IdelStateBase waitState;
        public IdelStateBase moveState;
        public IdelStateBase dragState;
        public IdelStateBase eggState;
        private MonsterIdel monsterTarget;

        private CancellationTokenSource _cancelToken;
        public MonsterIdelStateHandler(MonsterIdel monsterTarget, IdelStateBase waitState, IdelStateBase moveState, IdelStateBase dragState, IdelStateBase eggState)
        {
            this.monsterTarget = monsterTarget;
            this.waitState = waitState;
            this.moveState = moveState;
            this.dragState = dragState;
            this.eggState = eggState;
        }
        public void SwichState(IdelStateBase toState)
        {
            currentState?.OnExit();
            currentState = GameObject.Instantiate(toState);
            currentState.OnEnter(monsterTarget);
        }
        public void TransmisionHandle()
        {
            if (currentState == null && monsterTarget.isEgg == false)
            {
                Debug.Log("waitState");
                SwichState(waitState);
            }
            else if (currentState == null && monsterTarget.isEgg == true)
            {
                Debug.Log("eggState");
                SwichState(eggState);
            }

            monsterTarget.dragAndDropHandler.CheckDraging();
        }
        public async UniTask SwichStateByTime(float time, IdelStateBase toState)
        {
            _cancelToken = new CancellationTokenSource();
            try
            {
                await UniTask.WaitForSeconds(time, cancellationToken: _cancelToken.Token);
            }
            catch
            {
                return;
            }
            if (monsterTarget.gameObject != null)
            {
                Debug.Log("State by Time");
                SwichState(toState);
            }
        }
        public void CancelUniTask()
        {
            _cancelToken?.Cancel();
        }
    }
}