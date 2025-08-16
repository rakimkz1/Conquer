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
        private MonsterCollection monsterCollection;

        [Inject]
        public void Construct(ResourceManager resource, MonsterCollection collection)
        {
            this.resource = resource;
            monsterCollection = collection;
            LoadAllMonster();
        }

        public async UniTask<GameObject> SpawnMonsterInstance(PrefabKey key, MonsterIdelData data)
        {
            Vector3 pos = GetSpawnPosition();

            var handle = resource.InstantiateAsync(resource.so_Keys.GetKey(key), pos, Quaternion.identity);
            var install = await handle;
            monsterCollection.AddUnit(install.GetComponent<MonsterIdel>());
            install.GetComponent<MonsterIdel>().SetMonsterData(data);
            return install;
        }

        public void DestroyMonsterInstance(MonsterIdel target)
        {
            target.CancelUniTask();
            monsterCollection?.RemoveUnit(target);
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
        public bool SpawnMonster(int cost, PrefabKey key, MonsterIdelData data)
        {
            bool isAffordable = ManaManager.Instance.RemoveMana(cost);
            if (!isAffordable)
                return false;

            SpawnMonsterInstance(key, data);

            return true;
        }

        public async UniTask LoadAllMonster()
        {
            List<MonsterIdelData> dataList = monsterCollection.monsterUnitList;
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