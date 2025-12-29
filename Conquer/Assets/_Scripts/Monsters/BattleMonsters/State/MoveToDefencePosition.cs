namespace Monsters
{
    public class MoveToDefencePosition : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.defenceHandler.SetRow();
            target.defenceHandler.GetDefendePosition();
            target.viewMonster.animationManager.MoveAnimation();
        }

        public void OnExit(BattleMonster target)
        {
            if(target.stateMachine.movingToState is DefenceState == false)
            {
                target.defenceHandler.ExitFromRow();
            }
        }

        public void OnWork(BattleMonster target)
        {
            target.defenceHandler.MoveToDefencePosition();
        }
    }
}