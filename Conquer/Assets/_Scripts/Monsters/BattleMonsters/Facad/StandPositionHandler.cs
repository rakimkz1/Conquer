using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Monsters
{
    public class StandPositionHandler
    {
        private BattleMonster monster;
        private float speed;
        private float provocationDistance;
        private float maxTracingDistance;

        private Vector3 _keepingPosition;
        private CancellationTokenSource _cancelToken;

        public bool isMonsterInKeepingPosition;
        public StandPositionHandler(BattleMonster battleMonster, float speed, float provocationDistance, float maxTracingDistance)
        {
            monster = battleMonster;
            this.speed = speed;
            this.provocationDistance = provocationDistance;
            this.maxTracingDistance = maxTracingDistance;
        }

        public void RememberStayingPosition() => _keepingPosition = monster.transform.position;

        public void ReturnToPosition()
        {
            monster.movementHandler.MoveToTarget(monster.transform, _keepingPosition, speed, Time.deltaTime);
            if (monster.transform.position == _keepingPosition)
                isMonsterInKeepingPosition = true;
        }
        public bool IsTargetProvocationDistance()
        {
            if (monster.targetFinder.currentAttackTarget != null && Vector3.Distance(monster.transform.position, monster.targetFinder.currentAttackTarget.targetPosition.position) < provocationDistance)
                return true;
            return false;
        }
        public bool IsTargetInTracingDistance()
        {
            if (monster.targetFinder.currentAttackTarget != null && Vector3.Distance(_keepingPosition, monster.targetFinder.currentAttackTarget.targetPosition.position) < maxTracingDistance)
                return true;
            return false;
        }
        public void StopTargetLoopCheck() => _cancelToken?.Cancel();
        public async UniTask TargetLoopCheck()
        {
            _cancelToken = new CancellationTokenSource();
            while (_cancelToken.IsCancellationRequested == false)
            {
                monster.FindAttackTarget();
                try
                {
                    await UniTask.Delay(1000);
                }
                catch { return; }
            }
        }
    }
}