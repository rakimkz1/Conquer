using System;
using UnityEngine;

namespace BattleField
{
    public class AIPowerCanculator
    {
        private AttackableUnitsOnSceneCollection _attackCollecion;
        private float _armyProportionToDefence;
        private float _armyProportionToRetreat;
        private float _armyProportionToAttack;
        private float enemyArmyPowerScale;
        private float playerArmyPowerScale;
        public AIPowerCanculator(AttackableUnitsOnSceneCollection attackCollection)
        {
            _attackCollecion = attackCollection;
            _attackCollecion.OnPlayerAdded += target => playerArmyPowerScale += target.powerScale;
            _attackCollecion.OnPlayerRemoved += target => playerArmyPowerScale -= target.powerScale;
            _attackCollecion.OnEnemyAdded += target => enemyArmyPowerScale += target.powerScale;
            _attackCollecion.OnEnemyRemoved += target => enemyArmyPowerScale -= target.powerScale;
            CalculateArmyPower();
        }
        public void SetProperties(float armyProportionToDefence,float armyProportionToAttack , float armyProporionsToRetreat)
        {
            _armyProportionToAttack = armyProportionToAttack;
            _armyProportionToDefence = armyProportionToDefence;
            _armyProportionToRetreat = armyProporionsToRetreat;
        }
        private void CalculateArmyPower()
        {
            enemyArmyPowerScale = playerArmyPowerScale = 0f;
            foreach (var item in _attackCollecion.playerUnits)
                playerArmyPowerScale += item.powerScale;

            foreach (var item in _attackCollecion.enemyUnits)
                enemyArmyPowerScale += item.powerScale;
        }

        public bool isAttackPlayer()
        {
            return (enemyArmyPowerScale > playerArmyPowerScale * _armyProportionToAttack);
        }
        public bool isDefence()
        {
            return (enemyArmyPowerScale < playerArmyPowerScale * _armyProportionToDefence);
        }
        public bool isRetreat()
        {
            return (enemyArmyPowerScale < playerArmyPowerScale * _armyProportionToRetreat);
        }
    }
}