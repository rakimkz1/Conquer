namespace BattleField
{
    public class EnemyAIFactory
    {
        private EnemyAIProperties _aiProperties;
        private AIPowerCanculator _powerCanculator;
        private EnemyUnitBuymentHandler _buymentHandler;
        private EnemyCommandHandler _commandHandler;
        public EnemyAIFactory(LevelBuilder levelBuilder, AIPowerCanculator powerCanculator, EnemyUnitBuymentHandler buymentHandler, EnemyCommandHandler commandHandler)
        {
            _aiProperties = levelBuilder.CurrentSceneSettings.enemyAIProperties;
            _powerCanculator = powerCanculator;
            _buymentHandler = buymentHandler;
            _commandHandler = commandHandler;
            Create();
        }

        private void Create()
        {
            EnemyAI target = new EnemyAI(_buymentHandler, _commandHandler);
            _powerCanculator.SetProperties(_aiProperties.armyProportionToDefence, _aiProperties.armyProportionsToAttack, _aiProperties.armyProportionToRetreat);
            target.Init(_powerCanculator,new AIStateMachine(target, _aiProperties.stateSwitchColdown));
        }
    }
}
