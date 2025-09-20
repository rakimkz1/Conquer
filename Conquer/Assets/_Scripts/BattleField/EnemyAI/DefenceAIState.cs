namespace BattleField
{
    public class DefenceAIState : IEnemyAIState
    {
        public void Enter(EnemyAI enemyAI)
        {
            enemyAI.CommandDefence();
        }

        public void Exit(EnemyAI enemyAI)
        {
        }

        public void Work(EnemyAI enemyAI)
        {
        }
    }
}