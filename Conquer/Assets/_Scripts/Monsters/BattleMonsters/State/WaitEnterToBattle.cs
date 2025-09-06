namespace Monsters
{
    public class WaitEnterToBattle : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.retreatHandler.RequestEnterToBattle();
        }
        public void OnExit(BattleMonster target)
        {
            target.retreatHandler.ResetAllowmentEnter();
        }
        public void OnWork(BattleMonster target) { }
    }
}