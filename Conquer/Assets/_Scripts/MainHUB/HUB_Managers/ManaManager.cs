using MainHUB.Extractor;
using MainHUB.HUB_Managers;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets._Scripts.Managers
{
    public class ManaManager : MonoBehaviour
    {
        public ReactiveProperty<float> manaAmount = new ReactiveProperty<float>();

        private MonsterSpawnManager monsterSpawnManager;

        [Inject]
        public void Construct(MonsterSpawnManager monsterSpawnManager)
        {
            this.monsterSpawnManager = monsterSpawnManager;
        }

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
        public bool SpawnMonster(int cost, PrefabKey key)
        {
            bool isAffordable = RemoveMana(cost);
            if (!isAffordable)
                return false;

            monsterSpawnManager.SpawnMonster(key);

            return true;
        }
    }
}