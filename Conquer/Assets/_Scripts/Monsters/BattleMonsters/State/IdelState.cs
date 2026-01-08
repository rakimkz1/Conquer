using UnityEngine;

namespace Monsters
{
    public class IdelState : IBattleMonsterState
    {
        public void OnEnter(BattleMonster target)
        {
            target.standPositionHandler.TargetLoopCheck();
            target.standPositionHandler.RememberStayingPosition();
            target.viewMonster.animationManager?.IdelAnimation();
            Vector3 targetDiraction = target.targetFinder.currentAttackTarget.targetPosition.position;
            target.viewMonster.ChangeViewDirection(targetDiraction);
            Debug.DrawRay(target.transform.position, targetDiraction * 5f, Color.red, 3f);
        }

        public void OnExit(BattleMonster target)
        {
            target.standPositionHandler.isMonsterInKeepingPosition = false;
            target.standPositionHandler.StopTargetLoopCheck();
        }

        public void OnWork(BattleMonster target) { }
    }
}