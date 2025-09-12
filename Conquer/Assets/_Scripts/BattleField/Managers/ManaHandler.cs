using System;
using UniRx;

namespace BattleField
{
    public class ManaHandler
    {
        private float _currentManaAmount;
        public event Action<float> OnValue;

        public float GetManaAmount() => _currentManaAmount;

        public void AddMana(float number)
        {
            _currentManaAmount += number;
            OnValue?.Invoke(_currentManaAmount);
        }
        public bool SpendMana(float number)
        {
            if(_currentManaAmount < number)
                return false;
            _currentManaAmount -= number;
            OnValue?.Invoke(_currentManaAmount);
            return true;
        }
    }
}
