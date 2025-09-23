using BattleField;
using UnityEngine;

namespace Monsters
{
    public class DefenceHander
    {
        public bool isRowPlaceChanged { get; private set; }
        private BattleMonster monster;
        private ArmyStandRowHandler rowHandler;
        private float speed;
        private Row _denfenceStandRow;
        private Vector3 _defencePosition;
        private float _defenceTracingDistance;
        // need to be private
        public float _defenceProvocationDistance;

        public DefenceHander(BattleMonster battleMonster, ArmyStandRowHandler rowHandler, float speed, float defenceTracingDistance, float defenceProvocationDistance)
        {
            monster = battleMonster;
            this.rowHandler = rowHandler;
            this.speed = speed;
            _defenceTracingDistance = defenceTracingDistance;
            _defenceProvocationDistance = defenceProvocationDistance;
        }

        public void SetRow()
        {
            if (_denfenceStandRow != null)
                return;
            _denfenceStandRow = rowHandler.Add(monster);
            rowHandler.OnArmyRowChanged += RowPositionChanged;
        }

        public void GetDefendePosition()
        {
            isRowPlaceChanged = false;
            _defencePosition = rowHandler.GetPosition(monster, _denfenceStandRow);
        }

        public void ExitFromRow()
        {
            if (isRowPlaceChanged == true)
                return;
            rowHandler.OnArmyRowChanged -= RowPositionChanged;
            rowHandler.Remove(_denfenceStandRow, monster);
            _denfenceStandRow = null;
        }
        public void RowPositionChanged()
        {
            if (monster.stateMachine.currentArmyCommand == ArmyCommandTypes.Defence)
                isRowPlaceChanged = true;
        }

        public void MoveToDefencePosition()
        {
            Vector2 pos = monster.transform.position;
            monster.transform.position = monster.movementHandler.MoveToTarget(pos, _defencePosition, speed, Time.deltaTime);
        }
        public bool IsOnDefencePosition()
        {
            if (_defencePosition == monster.transform.position) return true;
            return false;
        }

        public bool IsDefenceTargetTracingDistance()
        {
            if (monster.targetFinder.currentAttackTarget == null)
                return false;
            float distance = Vector3.Distance(_defencePosition, monster.targetFinder.currentAttackTarget.targetPosition);
            return distance < _defenceTracingDistance ? true : false; 
        }

        public bool IsDefenceProvocationDistance()
        {
            if (monster.targetFinder.currentAttackTarget == null)
                return false;
            float distance = Vector3.Distance(_defencePosition, monster.targetFinder.currentAttackTarget.targetPosition);
            return distance < _defenceProvocationDistance ? true : false;
        }
    }
}