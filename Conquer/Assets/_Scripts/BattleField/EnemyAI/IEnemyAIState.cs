namespace BattleField
{
    public interface IEnemyAIState
    {
        public void Enter(EnemyAI enemyAI);
        public void Work(EnemyAI enemyAI);
        public void Exit(EnemyAI enemyAI);
    }
}
