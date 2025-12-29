namespace Monsters
{
    public class OutOfBattleState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.viewMonster.animationManager.MoveAnimation();
            target.retreatHandler.GoOutOfBattle();
        }
        public void OnExit(BattleMonster target)
        {
            target.retreatHandler.EnterToBattleFromRetreat();
        }
        public void OnWork(BattleMonster target) { }
    }
}