using UnityEngine;

namespace Monsters
{
    public class DefenceState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.viewMonster.animationManager?.IdelAnimation();
            Vector3 enemyDiraction = target.targetFinder.currentAttackTarget.targetPosition.position - target.transform.position;
            target.viewMonster.ChangeViewDirection(enemyDiraction);
            Debug.DrawRay(target.transform.position, enemyDiraction * 5f, Color.red, 2f);
        }

        public void OnExit(BattleMonster target)
        {
            target.defenceHandler.ExitFromRow();
        }

        public void OnWork(BattleMonster target) { }
    }
}