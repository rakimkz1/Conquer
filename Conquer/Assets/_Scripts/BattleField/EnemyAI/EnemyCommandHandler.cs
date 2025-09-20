using Cysharp.Threading.Tasks;
using Monsters;
using System;

namespace BattleField
{
    public class EnemyCommandHandler
    {
        private ArmyCommandHandler _commmandHandler;
        private EnemyAIProperties _properties;
        public event Action OnListenPlayerCommand;
        public EnemyCommandHandler(ArmyCommandHandler commmandHandler, LevelBuilder levelBuilder)
        {
            _commmandHandler = commmandHandler;
            _properties = levelBuilder.CurrentSceneSettings.enemyAIProperties;
            _commmandHandler.OnPlayerCommand += ListenPlayerCommand;
        }

        private async void ListenPlayerCommand(MonsterType type, ArmyCommandTypes types)
        {
            await UniTask.Delay((int)(_properties.playerCommandListenTime * 1000f));
            OnListenPlayerCommand?.Invoke();
        }

        public void AttackCommand()
        {
            for(int i = 0; i < 5; i++)
            {
                _commmandHandler.SayCommandEnemy((Monsters.MonsterType)i, ArmyCommandTypes.Attack);
            }
        }
        public void KeepPositionCommand()
        {
            for (int i = 0; i < 5; i++)
            {
                _commmandHandler.SayCommandEnemy((Monsters.MonsterType)i, ArmyCommandTypes.KeepPosition);
            }
        }
        public void DefenceCommand()
        {
            for (int i = 0; i < 5; i++)
            {
                _commmandHandler.SayCommandEnemy((Monsters.MonsterType)i, ArmyCommandTypes.Defence);
            }
        }
        public void RetreatCommand()
        {
            for (int i = 0; i < 5; i++)
            {
                _commmandHandler.SayCommandEnemy((Monsters.MonsterType)i, ArmyCommandTypes.Retreat);
            }
        }

    }
}