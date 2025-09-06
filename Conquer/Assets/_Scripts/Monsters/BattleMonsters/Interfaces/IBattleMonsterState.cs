namespace Monsters
{
    public interface IBattleMonsterState
    {
        public void OnEnter(BattleMonster target);
        public void OnWork(BattleMonster target);
        public void OnExit(BattleMonster target);
    }
}