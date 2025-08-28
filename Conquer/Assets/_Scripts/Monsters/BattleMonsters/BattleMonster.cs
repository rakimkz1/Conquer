using BattleField;
using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Monsters
{
    public class BattleMonster : MonoBehaviour, IAttackTarget
    {
        public BattleMonsterStateMachine stateMachine;
        public AttackTargetFinder targetFinder;
        public bool isEnemyUnit;
        [Header("Properties")]
        public float provocationDistance;
        public float maxTracingDistance;
        public float speed;
        public float attackDistance;
        public float attackSpeed;
        public bool isReadyToAttack = true;
        public Vector3 _keepingPosition;
        public event Action OnDead;
        public float attackPriority { get; set; }
        public Vector3 targetPosition { get; set; }

        private ArmyCommandHandler _armyCommandHandler;
        private AttackableUnitsOnSceneCollection _targetCollection;
        private Rigidbody2D _rb;

        [Inject]
        public void Construct(ArmyCommandHandler armyCommandHandler, AttackableUnitsOnSceneCollection targetCollection)
        {
            _armyCommandHandler = armyCommandHandler;
            _targetCollection = targetCollection;
            _armyCommandHandler.OnCommandToAllUnits += ListenArmyCommand;
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            targetFinder = new AttackTargetFinder(_targetCollection);
            stateMachine = new BattleMonsterStateMachine(this);
            if(isEnemyUnit)
                _targetCollection.AddEnemyUnit(this);
            else
                _targetCollection.AddPlayerUnit(this);
        }

        private void Update()
        {
            stateMachine.currentState?.OnWork(this);
            stateMachine.CheckAnyTransitions();
            stateMachine.CheckTransitions();
        }
        private void ListenArmyCommand(ArmyCommandTypes types)
        {
            stateMachine.ListenArmyCommand(types);
        }
        public void FindAttackTarget() => targetFinder.FindAttackTarget(!isEnemyUnit, transform.position);

        public void MoveToTarget()
        {
            Vector3 dir = (targetFinder.currentAttackTarget.targetPosition - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, _keepingPosition);
            _rb.linearVelocity = dir * speed;
        }
        public void StopMoving() => _rb.linearVelocity = Vector3.zero;

        public void RememberStayingPosition() => _keepingPosition = transform.position;

        public void ReturnToPosition()
        {
            Vector3 dir = (_keepingPosition - transform.position).normalized;
            float distance = Vector3.Distance(_keepingPosition, transform.position);
            _rb.linearVelocity = dir * speed;
        }

        public bool IsTargetAttackRange()
        {
            float distance = Vector3.Distance(transform.position, targetFinder.currentAttackTarget.targetPosition);
            if(distance < attackDistance)
                return true;
            return false;
        }
        public void Attack()
        {
            WaitAttackColdown();
        }

        private async UniTask WaitAttackColdown()
        {
            isReadyToAttack = false;
            await UniTask.Delay((int)(attackSpeed * 1000f));
            isReadyToAttack = true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, provocationDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, attackDistance);
        }
    }
}
