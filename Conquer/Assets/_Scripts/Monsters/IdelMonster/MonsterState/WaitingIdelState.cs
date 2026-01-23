using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Monsters.MonsterState
{
    [CreateAssetMenu(fileName = "WaitState", menuName = "ScriptableObjects/Monster/States/WaitIdel")]
    public class WaitingIdelState : IdelStateBase
    {
        [Range(0f, 10f)] public float minWaitTime;
        [Range(0f, 10f)] public float maxWaitTime;
        private MonsterIdel target;

        private void OnValidate()
        {
            maxWaitTime = Mathf.Max(maxWaitTime, minWaitTime);
        }

        public override void OnEnter(MonsterIdel target)
        {
            this.target = target;

            float time = Random.Range(minWaitTime, maxWaitTime);
            target.stateHandler.SwichStateByTime(time, target.stateHandler.moveState);
        }

        public override void OnExit()
        {

        }

        public override void OnWork()
        {
            
        }
    }
}