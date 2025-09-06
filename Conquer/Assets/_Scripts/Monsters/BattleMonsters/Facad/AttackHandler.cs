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
        private BattleMonster monster;
        private float attackSpeed;
        private float attackDistance;
        private float attackPreparationTime;
        private float speed;

        private CancellationTokenSource _cancel;
        public AttackHandler(BattleMonster battleMonster, float attackSpeed, float attackDistance, float attackPreparationTime, float speed)
        {
            monster = battleMonster;
            this.attackSpeed = attackSpeed;
            this.attackDistance = attackDistance;
            this.attackPreparationTime = attackPreparationTime;
            this.speed = speed;
        }

        public void Attack()
        {
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
            if (monster.targetFinder.currentAttackTarget == null)
                return false;
            float distance = Vector3.Distance(monster.transform.position, monster.targetFinder.currentAttackTarget.targetPosition);
            if (distance < attackDistance)
                return true;
            return false;
        }

        public void MoveToTarget()
        {
            Vector3 dir = (monster.targetFinder.currentAttackTarget.targetPosition - monster.transform.position).normalized;
            monster.transform.Translate(dir * speed * Time.deltaTime);
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
                await UniTask.Delay((int)(attackPreparationTime * 1000f));
            }
            catch { return; }
            isReadyToAttack = true;
        }
    }
}