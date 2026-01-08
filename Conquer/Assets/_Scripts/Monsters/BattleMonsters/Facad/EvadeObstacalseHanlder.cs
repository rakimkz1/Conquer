using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Monsters
{
    public class EvadeObstacalseHanlder
    {
        public bool IsEvadingObstacle { get; private set; }
        private float _speed;
        private float _obstacalCheckDistance;
        private BattleMonster _monster;
        private float _evadingTime;
        private float _stopAvoidTime;
        private float _evadingColdown;
        private CancellationTokenSource _cancellation;
        private bool _isEvadeColdown;

        public EvadeObstacalseHanlder(float speed, float obstacalCheckDistance, BattleMonster monster, float evadingTime, float evadingColdown, float stopAvoidTIme)
        {
            _speed = speed;
            _obstacalCheckDistance = obstacalCheckDistance;
            _monster = monster;
            _evadingTime = evadingTime;
            _stopAvoidTime = stopAvoidTIme;
            _evadingColdown = evadingColdown;
            Init();
        }

        private void Init()
        {
            _monster.OnDead += StopChecking;
            StartCheck();
        }

        private async void LoopObstaclesCheck()
        {
            _cancellation = new CancellationTokenSource();
            while (!_cancellation.IsCancellationRequested)
            {
                CheckObstacle();
                try
                {
                    await UniTask.Delay(400, cancellationToken: _cancellation.Token);
                }
                catch{ return; }
            } 
        }

        private void CheckObstacle()
        {
            if (IsEvadingObstacle || _monster.movementHandler == null || _isEvadeColdown)
                return;
            Vector3 dir = _monster.movementHandler.GetDiraction();
            RaycastHit2D[] hit = Physics2D.RaycastAll(_monster.transform.position, dir);
            Debug.DrawRay(_monster.transform.position, dir * _obstacalCheckDistance, Color.green, 0.2f);
            for (int i = 0; i < hit.Length; i++)
            {
                BattleMonster target = hit[i].collider?.GetComponent<BattleMonster>();
                if(target != null && Vector2.Distance(target.targetPosition.position, _monster.targetPosition.position) < _obstacalCheckDistance && target != _monster)
                {
                    WaitEvadingTime();
                    return;
                }
            }
        }

        private async void WaitEvadingTime()
        {
            IsEvadingObstacle = true;
            await UniTask.Delay((int)(_evadingTime * 1000f));
            IsEvadingObstacle = false;
            WaitEvadeColdown();
        }
        private async void WaitEvadeColdown()
        {
            _isEvadeColdown = true;
            await UniTask.Delay((int)(_evadingColdown * 1000f));
            _isEvadeColdown = false;
        }

        public void EvadeTheObstacle()
        {
            Vector3 dir = Vector3.up * (_monster.targetPosition.position.y > 0f ? 1f : -1f);
            _monster.movementHandler.MoveToTarget(_monster.transform, _monster.transform.position + dir, _speed * 0.6f, Time.deltaTime);
        }

        public void StopChecking(IAttackTarget target)
        {
            _cancellation?.Cancel();
        }
        public void StartCheck()
        {
            LoopObstaclesCheck();
        }
    }
}