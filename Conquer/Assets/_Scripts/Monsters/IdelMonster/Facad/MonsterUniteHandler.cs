using UnityEngine;

namespace Monsters
{
    public class MonsterUniteHandler
    {
        private MonsterIdel _targetMonster;
        public MonsterUniteHandler(MonsterIdel targetMonster)
        {
            _targetMonster = targetMonster;
        }

        public void CheckIsUnity()
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(_targetMonster.transform.position, Vector2.zero);

            for (int i = 0; i < hit.Length; i++)
            {
                MonsterIdel target = hit[i].collider.gameObject.GetComponent<MonsterIdel>();
                if (target != null && target != _targetMonster && target.monsterType == _targetMonster.monsterType && target.monsterLevel == _targetMonster.monsterLevel)
                    UnityMonster(target);
            }
        }

        private void UnityMonster(MonsterIdel target)
        {
            _targetMonster.monsterLevel++;
            _targetMonster.monsterSpawn.UnityTwoMonsters(_targetMonster, target);
        }
    }
}