namespace Monsters
{
    public class MoveToTargetState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.viewMonster.animationManager.MoveAnimation();
            target.FindAttackTarget();
        }
        public void OnExit(BattleMonster target) { }
        public void OnWork(BattleMonster target)
        {
            target.attackHandler.MoveToTarget();
        }
    }
}