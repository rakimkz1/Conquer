using UnityEngine;

namespace Monsters.IdelMonster
{
    public class MonsterMovementHandler
    {
        private MonsterIdel _targetMonster;
        public MonsterMovementHandler(MonsterIdel targetMonster)
        {
            _targetMonster = targetMonster;
        }

        public void WanderToDiraction(float speed, Vector2 diraction)
        {
            _targetMonster.MoveDirection(diraction * speed);
        }
    }
}