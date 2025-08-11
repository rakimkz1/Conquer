using Assets._Scripts.Managers;
using UnityEngine;
using Zenject;

namespace MainHUB.HUB_Managers
{
    public class MonsterSpawnManager : MonoBehaviour
    {
        public float spawnRadious;
        private ResourceManager resource;

        [Inject]
        public void Construct(ResourceManager resource)
        {
            this.resource = resource;
        }

        public void SpawnMonsterInstance(PrefabKey key)
        {
            Vector3 pos = GetSpawnPosition();

            resource.InstantiateAsync(resource.so_Keys.GetKey(key), pos, Quaternion.identity);
        }

        private Vector2 GetSpawnPosition()
        {
            float randomDistance = Random.Range(0f, spawnRadious);
            float randomAngle = Random.Range(0f, 360f);

            Vector2 pos = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * randomDistance;
            return pos;
        }
        public bool SpawnMonster(int cost, PrefabKey key)
        {
            bool isAffordable = ManaManager.Instance.RemoveMana(cost);
            if (!isAffordable)
                return false;

            SpawnMonsterInstance(key);

            return true;
        }
    }
}