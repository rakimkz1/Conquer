namespace Monsters
{
    public class ReturnToPositionState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target) 
        {
            target.viewMonster.animationManager?.MoveAnimation();
        }

        public void OnExit(BattleMonster target) { }

        public void OnWork(BattleMonster target)
        {
            target.standPositionHandler.ReturnToPosition();
        }
    }
}
