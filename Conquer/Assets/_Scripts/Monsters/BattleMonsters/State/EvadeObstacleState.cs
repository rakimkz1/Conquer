namespace Monsters
{
    public class EvadeObstacleState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target) {}
        public void OnExit(BattleMonster target) { }
        public void OnWork(BattleMonster target)
        {
            target.evadeHandler.EvadeTheObstacle();
        }
    }
}
