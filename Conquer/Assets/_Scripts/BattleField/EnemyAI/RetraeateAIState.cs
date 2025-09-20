namespace BattleField
{
    public class RetraeateAIState : IEnemyAIState
    {
        public void Enter(EnemyAI enemyAI)
        {
            enemyAI.CommandRetreat();
        }

        public void Exit(EnemyAI enemyAI)
        {
        }

        public void Work(EnemyAI enemyAI)
        {
        }
    }
}