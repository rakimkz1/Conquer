using BattleField;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Monsters
{
    public class BattleMonster : MonoBehaviour, IAttackTarget
    {
        public BattleMonsterStateMachine stateMachine;
        public AttackTargetFinder targetFinder;
        public HealthHandler healthHandler;
        public AttackHandler attackHandler;
        public DefenceHander defenceHandler;
        public StandPositionHandler standPositionHandler;
        public RetreatHandler retreatHandler;
        public bool isEnemyUnit;
        [Header("Properties")]
        public int monsterLevel;
        public MonsterType monsterType;
        public event Action OnDead;
        public float attackPriority { get; set; }
        public Vector3 targetPosition { get; set; }
        public AttackableUnitsOnSceneCollection _targetCollection;
        private ArmyCommandHandler _commandHandler;

        [Inject]
        public void Construct(AttackableUnitsOnSceneCollection targetCollection, ArmyCommandHandler commandHandler)
        {
            _targetCollection = targetCollection;
            _commandHandler = commandHandler;
        }
        private void Start()
        {
            targetFinder = new AttackTargetFinder(_targetCollection);
            stateMachine = new BattleMonsterStateMachine(this);
            if (isEnemyUnit)
                _targetCollection.AddEnemyUnit(this);
            else
                _targetCollection.AddPlayerUnit(this);
            
            if (!_commandHandler.OnCommand.ContainsKey(monsterType))
                _commandHandler.OnCommand[monsterType] = null;

            _commandHandler.OnCommand[monsterType] += ListenArmyCommand;
        }
        private void Update()
        {
            stateMachine.currentState?.OnWork(this);
            stateMachine.CheckAnyTransitions();
            stateMachine.CheckTransitions();
            targetPosition = transform.position;
        }
        public void ListenArmyCommand(ArmyCommandTypes types) => stateMachine.ListenArmyCommand(types);
        public void FindAttackTarget() => targetFinder.FindAttackTarget(!isEnemyUnit, transform.position);
        public bool IsTargetAttackRange() => attackHandler.IsTargetAttackRange();
        public bool isCapableToAttack() => attackHandler.isCapableToAttack;
        public bool isReadyToAttack() => attackHandler.isReadyToAttack;
        public bool IsOnDefencePosition() => defenceHandler.IsOnDefencePosition();
        public bool isRowPlaceChanged() => defenceHandler.isRowPlaceChanged;
        public bool IsTargetProvocationDistance() => standPositionHandler.IsTargetInTracingDistance();
        public bool IsTargetInTracingDistance() => standPositionHandler.IsTargetInTracingDistance();
        public bool isMonsterInKeepingPosition() => standPositionHandler.isMonsterInKeepingPosition;
        public bool IsOutOfBattle() => retreatHandler.IsOutOfBattle();
        public bool isAllowedToEnterBattle() => retreatHandler.isAllowedToEnterBattle;


        public void Dead()
        {
            _commandHandler.OnCommand[monsterType] -= ListenArmyCommand;
        }

    }
}
