using BattleField;
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
        public EvadeObstacalseHanlder evadeHandler;
        public GridMovementHandler movementHandler;
        public bool isEnemyUnit;
        [Header("Properties")]
        public int monsterLevel;
        public MonsterType monsterType;
        public event Action<IAttackTarget> OnDead;
        public event Action<IAttackTarget> OnExitTargetCollection;

        public float attackPriority { get; set; }
        public Vector3 targetPosition { get; set; }
        public float powerScale { get; set; }
        public bool isDead { get; set; }

        public AttackableUnitsOnSceneCollection _targetCollection;
        private UnitsCommandKeeper _commandKeeper;

        [Inject]
        public void Construct(AttackableUnitsOnSceneCollection targetCollection, UnitsCommandKeeper commandKeeper)
        {
            _targetCollection = targetCollection;
            _commandKeeper = commandKeeper;
        }
        public void Init()
        {
            targetFinder = new AttackTargetFinder(_targetCollection, this);
            stateMachine = new BattleMonsterStateMachine(this, _commandKeeper);
            movementHandler = new GridMovementHandler();
            if (isEnemyUnit)
            {
                _commandKeeper.OnEnemyCommand += ListenArmyCommand;
                _targetCollection.AddEnemyUnit(this);
            }
            else
            {
                _commandKeeper.OnPlayerCommand += ListenArmyCommand;
                _targetCollection.AddPlayerUnit(this);
            }

            healthHandler.OnDead += Dead;
            retreatHandler.OnRetreat += () => OnExitTargetCollection?.Invoke(this);
        }
        private void Update()
        {
            stateMachine.currentState?.OnWork(this);
            stateMachine.CheckAnyTransitions();
            stateMachine.CheckTransitions();
            targetPosition = transform.position;
        }
        public void ListenArmyCommand(ArmyCommandTypes commandType, MonsterType monsterType) => stateMachine.ListenArmyCommand(commandType, monsterType);
        public void TakeDamage(float damage) => healthHandler.TakeDamage(damage);
        public void FindAttackTarget() => targetFinder.FindAttackTarget();
        public bool IsTargetAttackRange() => attackHandler.IsTargetAttackRange();
        public bool isCapableToAttack() => attackHandler.isCapableToAttack;
        public bool isReadyToAttack() => attackHandler.isReadyToAttack;
        public bool IsOnDefencePosition() => defenceHandler.IsOnDefencePosition();
        public bool isRowPlaceChanged() => defenceHandler.isRowPlaceChanged;
        public bool IsTargetProvocationDistance() => standPositionHandler.IsTargetInTracingDistance();
        public bool IsTargetInTracingDistance() => standPositionHandler.IsTargetInTracingDistance();
        public bool IsDefenceProvocationDistance()
        {
            return defenceHandler.IsDefenceProvocationDistance();
        }

        public bool IsDefenceTargetInTracingDistance() => defenceHandler.IsDefenceTargetTracingDistance();
        public bool isMonsterInKeepingPosition() => standPositionHandler.isMonsterInKeepingPosition;
        public bool IsOutOfBattle() => retreatHandler.IsOutOfBattle();
        public bool isAllowedToEnterBattle() => retreatHandler.isAllowedToEnterBattle;


        public void Dead()
        {
            if (isEnemyUnit)
            {
                _commandKeeper.OnEnemyCommand -= ListenArmyCommand;
                _targetCollection.RemoveEnemyUnit(this);
            }
            else
            {
                _commandKeeper.OnPlayerCommand -= ListenArmyCommand;
                _targetCollection.RemovePlayerUnit(this);
            }
            int count = OnDead?.GetInvocationList().Length ?? 0;
            OnDead?.Invoke(this);
            OnDead = null;
            gameObject.SetActive(false);
        }
        [ContextMenu("Get current state")]
        private void GetInfo()
        {
            Debug.Log(stateMachine.currentState.ToString());
        }
    }
}
