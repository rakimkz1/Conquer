using Monsters;
using System.Collections.Generic;

namespace BattleField
{
    public class UnitsAliveChecker
    {
        private MonsterSpawnHandler _monsterSpawnHanlder;
        private MonsterUnitFactory _factory;
        private List<BattleMonster> _aliveMonsters = new List<BattleMonster>();
        public UnitsAliveChecker(MonsterSpawnHandler monsterSpawnHanlder, MonsterUnitFactory factory)
        {
            _monsterSpawnHanlder = monsterSpawnHanlder;
            _factory = factory;
            factory.OnMonsterCreate += AddBattleMonster;
        }
        public void AddBattleMonster(BattleMonster target)
        {
            _aliveMonsters.Add(target);
            target.OnDead += OnMonsterDied;
        }
        private void OnMonsterDied(IAttackTarget target)
        {
            _aliveMonsters.Remove(target as BattleMonster);
        }
        public List<MonsterIdelData> GetSurvivedMonsterCollection()
        {
            List<MonsterIdelData> answer = new List<MonsterIdelData>();
            for(int i = 0; i < _aliveMonsters.Count; i++)
            {
                MonsterIdelData data = new MonsterIdelData(_aliveMonsters[i].monsterLevel, _aliveMonsters[i].monsterType);
                answer.Add(data);
            }
            answer.AddRange(_monsterSpawnHanlder.GetMonsterCollection());
            return answer;
        }
    }
}
