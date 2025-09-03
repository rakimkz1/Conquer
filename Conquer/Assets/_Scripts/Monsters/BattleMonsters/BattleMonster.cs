using BattleField;
using Cysharp.Threading.Tasks;
using System;
using System.IO.Compression;
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
        public int monsterLevel;
        public MonsterType monsterType;
        public float provocationDistance;
        public float maxTracingDistance;
        public float speed;
        public float attackDistance;
        public float attackSpeed;
        public float attackPreparationTime;
        public bool isReadyToAttack = true;
        public Vector3 _keepingPosition;
        public event Action OnDead;
        public ArmyStandRowHandler rowHandler;
        public float attackPriority { get; set; }
        public Vector3 targetPosition { get; set; }
        public bool isRowPlaceChanged { get; private set; }
        public bool isMonsterInKeepingPosition;
        private ArmyCommandHandler _armyCommandHandler;
        private AttackableUnitsOnSceneCollection _targetCollection;
        private ArmyStandRowHandler.Row _denfenceStandRow;
        private Vector3 _defencePosition;
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
        }
        public void ListenArmyCommand(ArmyCommandTypes types)
        {
            stateMachine.ListenArmyCommand(types);
        }
        public void FindAttackTarget() => targetFinder.FindAttackTarget(!isEnemyUnit, transform.position);

        public void MoveToTarget()
        {
            Vector3 dir = (targetFinder.currentAttackTarget.targetPosition - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }

        public void RememberStayingPosition() => _keepingPosition = transform.position;

        public void ReturnToPosition()
        {
            transform.position = Vector3.MoveTowards(transform.position, _keepingPosition, speed * Time.deltaTime);
            if (transform.position == _keepingPosition)
                isMonsterInKeepingPosition = true;
        }

        public bool IsTargetAttackRange()
        {
            if (targetFinder.currentAttackTarget == null)
                return false;
            float distance = Vector3.Distance(transform.position, targetFinder.currentAttackTarget.targetPosition);
            if(distance < attackDistance)
                return true;
            return false;
        }
        public void Attack()
        {
            WaitAttackColdown();
        }

        public void SetRow()
        {
            if (_denfenceStandRow != null)
                return;
            _denfenceStandRow = rowHandler.Add(this);
            rowHandler.OnArmyRowChanged += RowPositionChanged;
        }

        public void GetDefendePosition()
        {
            isRowPlaceChanged = false;
            _defencePosition = rowHandler.GetPosition(this, _denfenceStandRow);
        }

        public void ExitFromRow()
        {
            if (isRowPlaceChanged == true)
                return;
            rowHandler.Remove(_denfenceStandRow, this);
            rowHandler.OnArmyRowChanged -= RowPositionChanged;
            _denfenceStandRow = null;
        }
        public void RowPositionChanged() => isRowPlaceChanged = true;

        public void MoveToDefencePosition()
        {
            transform.position = Vector3.MoveTowards(transform.position, _defencePosition, speed * Time.deltaTime);
        }
        private async UniTask WaitAttackColdown()
        {
            isReadyToAttack = false;
            await UniTask.Delay((int)(attackSpeed * 1000f));
            isReadyToAttack = true;
        }
        public bool IsOnDefencePosition()
        {
            if(_defencePosition == transform.position) return true;
            return false;
        }
        public bool IsTargetProvocationDistance()
        {
            if(targetFinder.currentAttackTarget != null && Vector3.Distance(transform.position, targetFinder.currentAttackTarget.targetPosition) < provocationDistance) 
                return true;
            return false;
        }
        public bool IsTargetInTracingDistance()
        {
            if (targetFinder.currentAttackTarget != null && Vector3.Distance(_keepingPosition, targetFinder.currentAttackTarget.targetPosition) < maxTracingDistance)
                return true;
            return false;
        }
        public void Dead()
        {
            _armyCommandHandler.OnCommand[monsterType] -= ListenArmyCommand;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, provocationDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, attackDistance);
            if(_keepingPosition != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(_keepingPosition, 0.2f);
            }
        }

    }
}
