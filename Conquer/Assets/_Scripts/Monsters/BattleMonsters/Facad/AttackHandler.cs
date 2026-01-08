using BattleField;
using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Monsters
{
    public class AttackHandler
    {
        public bool isCapableToAttack = true;
        public bool isReadyToAttack;
        private IMonsterAttackType attackType;
        private BattleMonster monster;
        private float attackSpeed;
        private float attackDistance;
        private float attackPreparationTime;
        private float speed;

        private CancellationTokenSource _cancel;
        public AttackHandler(BattleMonster battleMonster, IMonsterAttackType attackType, float attackSpeed, float attackDistance, float attackPreparationTime, float speed)
        {
            monster = battleMonster;
            this.attackType = attackType;
            this.attackSpeed = attackSpeed;
            this.attackDistance = attackDistance;
            this.attackPreparationTime = attackPreparationTime;
            this.speed = speed;
        }

        public void Attack()
        {
            if (monster.monsterType == MonsterType.Sprinter || monster.monsterType == MonsterType.Rangers || monster.monsterType == MonsterType.Sieges)
                attackType.InitAttack(monster.targetFinder.currentAttackTarget, monster.targetPosition.position, new MonsterIdelData(monster.monsterLevel, monster.monsterType));
            else
                attackType.InitAttack(monster.targetFinder.currentAttackTarget.targetPosition.position, monster.targetPosition.position, new MonsterIdelData(monster.monsterLevel, monster.monsterType));

            attackType.Attack();
            WaitAttackColdown();
        }
        private async UniTask WaitAttackColdown()
        {
            isCapableToAttack = false;
            await UniTask.Delay((int)(attackSpeed * 1000f));
            isCapableToAttack = true;
        }
        public bool IsTargetAttackRange()
        {
            if (monster.targetFinder.currentAttackTarget == null || monster.targetFinder.currentAttackTarget.isDead)
                return false;
            float distance = Vector3.Distance(monster.transform.position, monster.targetFinder.currentAttackTarget.targetPosition.position);
            if (distance < attackDistance)
                return true;
            return false;
        }

        public void MoveToTarget()
        {
            Vector2 target = monster.targetFinder.currentAttackTarget.targetPosition.position;
            monster.movementHandler.MoveToTarget(monster.transform, target, speed, Time.deltaTime);
        }
        public void StopPreparation()
        {
            _cancel?.Cancel();
        }
        public async UniTask WaitAttackPreparation()
        {
            _cancel = new CancellationTokenSource();
            isReadyToAttack = false;
            try
            {
                await UniTask.Delay((int)(attackPreparationTime * 1000f), cancellationToken: _cancel.Token);
            }
            catch { return; }
            isReadyToAttack = true;
        }
    }
}