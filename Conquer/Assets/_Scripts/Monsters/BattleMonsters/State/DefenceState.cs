namespace Monsters
{
    public class DefenceState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target) 
        {
            target.viewMonster.animationManager.IdelAnimation();
        }

        public void OnExit(BattleMonster target)
        {
            target.defenceHandler.ExitFromRow();
        }

        public void OnWork(BattleMonster target) { }
    }
}