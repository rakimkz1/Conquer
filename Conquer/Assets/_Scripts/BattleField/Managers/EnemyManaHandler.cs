using System;
using UniRx;
using UnityEngine;

namespace BattleField
{
    public class EnemyManaHandler
    {
        public float manaAmount;
        public event Action<float> OnManaAdded;
        public void AddMana(float number)
        {
            manaAmount += number;
            OnManaAdded?.Invoke(manaAmount);
        } 

        public bool SpendMana(float number)
        {
            if(manaAmount - number < 0f)
                return false;
            manaAmount -= number;
            return true;
        }
    }
}
