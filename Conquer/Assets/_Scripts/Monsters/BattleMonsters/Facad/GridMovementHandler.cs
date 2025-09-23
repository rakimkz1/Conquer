using System;
using UnityEngine;

namespace Monsters
{
    public class GridMovementHandler
    {
        private Vector3 _diraction;
        public Vector3 MoveToTarget(Vector3 pos, Vector3 target, float speed, float deltaTime)
        {
            Vector3 dir = FindDirection(pos, target);
            Vector3 answer = pos + dir * speed * deltaTime;
            if((pos.y < target.y) != (answer.y < target.y))
                answer.y = target.y;
            if ((pos.x < target.x) != (answer.x < target.x))
                answer.x = target.x;
            return answer;
        }

        private Vector3 FindDirection(Vector3 pos, Vector3 target)
        {
            Vector2 dir = (target - pos);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle = Mathf.RoundToInt(angle / 45f) * 45f;
            _diraction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);
            return _diraction;
        }
        public Vector3 GetDiraction() => _diraction;
    }
}