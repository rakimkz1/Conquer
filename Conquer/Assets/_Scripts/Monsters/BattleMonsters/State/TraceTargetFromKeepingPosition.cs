namespace Monsters
{
    public class TraceTargetFromKeepingPosition : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target) { }

        public void OnExit(BattleMonster target) { }

        public void OnWork(BattleMonster target)
        {
            target.attackHandler.MoveToTarget();
        }
    }
}