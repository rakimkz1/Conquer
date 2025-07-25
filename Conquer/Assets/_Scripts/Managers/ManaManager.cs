using UniRx;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class ManaManager : MonoBehaviour
    {
        public ReactiveProperty<float> manaAmount = new ReactiveProperty<float>();

        public void Init() { }

        public void AddMana(float number)
        {
            manaAmount.Value += number;
        }

        public bool RemoveMana(float number)
        {
            if(manaAmount.Value - number >= 0)
            {
                manaAmount.Value -= number;
                return true;
            }
            return false;
        }
    }
}