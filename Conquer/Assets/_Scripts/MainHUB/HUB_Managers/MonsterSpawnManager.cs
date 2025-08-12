using Assets._Scripts.Managers;
using Cysharp.Threading.Tasks;
using Monsters;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MainHUB.HUB_Managers
{
    public class MonsterSpawnManager : MonoBehaviour
    {
        public float spawnRadious;
        private ResourceManager resource;
        private MonsterCollection so_monsterCollection;

        [Inject]
        public void Construct(ResourceManager resource, MonsterCollection collection)
        {
            this.resource = resource;
            so_monsterCollection = collection;
        }

        public async UniTask<GameObject> SpawnMonsterInstance(PrefabKey key, MonsterCollection.MonsterIdelData data)
        {
            Vector3 pos = GetSpawnPosition();

            var handle = resource.InstantiateAsync(resource.so_Keys.GetKey(key), pos, Quaternion.identity);
            var install = await handle;
            so_monsterCollection.AddUnit(install.GetComponent<MonsterIdel>());
            install.GetComponent<MonsterIdel>().SetMonsterData(data);
            return install;
        }

        public void DestroyMonsterInstance(MonsterIdel target)
        {
            target.CancelUniTask();
            so_monsterCollection?.RemoveUnit(target);
            Destroy(target.gameObject);
        }

        private Vector2 GetSpawnPosition()
        {
            float randomDistance = Random.Range(0f, spawnRadious);
            float randomAngle = Random.Range(0f, 360f);

            Vector2 midPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.25f, Screen.height * 0.5f));
            Vector2 pos = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * randomDistance;
            return pos + midPos;
        }
        public bool SpawnMonster(int cost, PrefabKey key, MonsterCollection.MonsterIdelData data)
        {
            bool isAffordable = ManaManager.Instance.RemoveMana(cost);
            if (!isAffordable)
                return false;

            SpawnMonsterInstance(key, data);

            return true;
        }

        public async UniTask LoadAllMonster()
        {
            List<MonsterCollection.MonsterIdelData> dataList = so_monsterCollection.monsterUnitList;
            PrefabKey key = PrefabKey.MonsterIdel;
            for (int i = 0; i < dataList.Count; i++)
            {
                GameObject target = await SpawnMonsterInstance(key, dataList[i]);
            }
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth * 0.25f, Camera.main.pixelHeight * 0.5f)), spawnRadious);
        }
    }
}