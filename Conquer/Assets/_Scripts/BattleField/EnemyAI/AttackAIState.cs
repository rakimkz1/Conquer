namespace BattleField
{
    public class AttackAIState : IEnemyAIState
    {
        public void Enter(EnemyAI enemyAI)
        {
            enemyAI.CommandAttack();
        }

        public void Exit(EnemyAI enemyAI)
        {
        }

        public void Work(EnemyAI enemyAI)
        {
        }
    }
}