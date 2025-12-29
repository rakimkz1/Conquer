namespace Monsters
{
    public class RetreatState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.viewMonster.animationManager.MoveAnimation();
        }

        public void OnExit(BattleMonster target) { }

        public void OnWork(BattleMonster target)
        {
            target.retreatHandler.MoveToRetreatPoint();
        }
    }
}