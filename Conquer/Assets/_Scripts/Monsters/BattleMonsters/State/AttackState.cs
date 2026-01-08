namespace Monsters
{
    public class AttackState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.attackHandler.Attack();
            target.viewMonster.animationManager.AttackAnimation();
        }

        public void OnExit(BattleMonster target) { }

        public void OnWork(BattleMonster target) { }
    }
}