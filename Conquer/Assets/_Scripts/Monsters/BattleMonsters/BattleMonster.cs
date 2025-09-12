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
        public event Action<IAttackTarget> OnDead;
        public float attackPriority { get; set; }
        public Vector3 targetPosition { get; set; }
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
            targetFinder = new AttackTargetFinder(_targetCollection);
            stateMachine = new BattleMonsterStateMachine(this, _commandKeeper);
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
            OnDead?.Invoke(this);
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
            Destroy(gameObject);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackHandler.attackDistance);
        }

    }
}
