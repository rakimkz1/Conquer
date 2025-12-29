namespace Monsters
{
    public class IdelState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.standPositionHandler.TargetLoopCheck();
            target.standPositionHandler.RememberStayingPosition();
            target.viewMonster.animationManager.IdelAnimation();
        }

        public void OnExit(BattleMonster target)
        {
            target.standPositionHandler.isMonsterInKeepingPosition = false;
            target.standPositionHandler.StopTargetLoopCheck();
        }

        public void OnWork(BattleMonster target) { }
    }
}