using BattleField;
using UnityEngine;

namespace Monsters
{
    public class RetreatHandler
    {
        private BattleMonster monster;
        private EnterToBattleInRowHandler enterToBattleInRowHandler;
        private Transform outOfBattlePoint;
        private float speed;

        public bool isAllowedToEnterBattle { get; private set; }
        public RetreatHandler(BattleMonster battleMonster, EnterToBattleInRowHandler enterToBattleInRowHandler, Transform outOfBattlePoint, float speed)
        {
            monster = battleMonster;
            this.enterToBattleInRowHandler = enterToBattleInRowHandler;
            this.outOfBattlePoint = outOfBattlePoint;
            this.speed = speed;
        }
        public void MoveToRetreatPoint()
        {
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, outOfBattlePoint.position, speed * Time.deltaTime);
        }

        public void GoOutOfBattle()
        {
            if (monster.isEnemyUnit)
                monster._targetCollection.RemoveEnemyUnit(monster);
            else
                monster._targetCollection.RemovePlayerUnit(monster);
            monster.healthHandler.GoOutOfBattle();
            enterToBattleInRowHandler.Add(monster);
        }
        public void EnterToBattleFromRetreat()
        {
            if (monster.isEnemyUnit)
                monster._targetCollection.AddEnemyUnit(monster);
            else
                monster._targetCollection.AddPlayerUnit(monster);
            monster.healthHandler.EnterToBattle();
        }


        public void AllowedEnterToBattle(Vector3 pos)
        {
            monster.transform.position = pos;
            isAllowedToEnterBattle = true;
        }
        public void ResetAllowmentEnter() => isAllowedToEnterBattle = false;
        public void RequestEnterToBattle()
        {
            enterToBattleInRowHandler.RequestToEnterBattle(monster.monsterType);
        }

        public bool IsOutOfBattle()
        {
            if (monster.transform.position == outOfBattlePoint.position)
                return true;
            return false;
        }
    }
}