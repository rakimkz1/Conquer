using System;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Monsters
{
    public class GridMovementHandler : IDisposable
    {
        private Vector3 _diraction;
        private TransformAccessArray transformAccess;
        public void MoveToTarget(Transform pos, Vector3 target, float speed, float deltaTime)
        {
            Vector3 initialPos = pos.position;
            transformAccess = new TransformAccessArray(new Transform[] { pos });
            MoveJob job = new MoveJob
            {
                target = target,
                speed = speed,
                deltaTime = deltaTime
            };
            JobHandle handler = job.Schedule(transformAccess);
            handler.Complete();
            transformAccess.Dispose();
            _diraction = pos.position - initialPos;
        }
        public Vector3 GetDiraction() => _diraction;

        public void Dispose()
        {
            if(transformAccess.isCreated)
                transformAccess.Dispose();
        }

        public struct MoveJob : IJobParallelForTransform
        {
            public Vector3 target;
            public float speed;
            public float deltaTime;
            public void Execute(int index, TransformAccess transform)
            {
                Vector3 dir = FindDirection(transform.position, target);
                Vector3 answer = transform.position + dir * speed * deltaTime;
                if ((transform.position.y < target.y) != (answer.y < target.y))
                    answer.y = target.y;
                if ((transform.position.x < target.x) != (answer.x < target.x))
                    answer.x = target.x;
                transform.position = answer;
            }
            private Vector3 FindDirection(Vector3 pos, Vector3 target)
            {
                Vector2 dir = (target - pos);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                angle = Mathf.RoundToInt(angle / 45f) * 45f;
                return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);
            }
        }
    }
}