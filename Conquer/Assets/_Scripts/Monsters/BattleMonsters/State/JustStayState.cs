namespace Monsters
{
    public class JustStayState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target) 
        {
            target.viewMonster.animationManager.IdelAnimation();
        }

        public void OnExit(BattleMonster target) { }

        public void OnWork(BattleMonster target) { }
    }
}