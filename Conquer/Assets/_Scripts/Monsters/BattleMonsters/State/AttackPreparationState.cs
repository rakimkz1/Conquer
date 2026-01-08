namespace Monsters
{
    public class AttackPreparationState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.attackHandler.WaitAttackPreparation();
            target.viewMonster.animationManager?.PreparationAttackAnimation();
        }


        public void OnExit(BattleMonster target)
        {
            target.attackHandler.StopPreparation();
        }

        public void OnWork(BattleMonster target) { }
    }
}