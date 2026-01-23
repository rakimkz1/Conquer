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

        public async UniTask<GameObject> SpawnMonsterInstance(string resourcePath , MonsterIdelData data, bool isAddToCollection)
        {
            Vector3 pos = GetSpawnPosition();

            var handle = resource.InstantiateAsync(resourcePath, pos, Quaternion.identity);
            var install = await handle;
            install.GetComponent<MonsterIdel>().SetMonsterData(data);
            if(isAddToCollection) 
                monsterCollection.AddUnit(install.GetComponent<MonsterIdel>());
            return install;
        }

        public void UnityTwoMonsters(MonsterIdel mainMonsterIdel, MonsterIdel secondaryMonsterIdel)
        {

            monsterCollection.RemoveUnit(secondaryMonsterIdel);
            monsterCollection.AddUnit(mainMonsterIdel);
            DestroyMonsterInstance(secondaryMonsterIdel);
        }
        public void DestroyMonsterInstance(MonsterIdel target)
        {
            target.CancelStateTimer();
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
        public bool SpawnMonster(int cost, string resourcePath, MonsterIdelData data)
        {
            bool isAffordable = ManaManager.Instance.RemoveMana(cost);
            if (!isAffordable)
                return false;

            SpawnMonsterInstance(resourcePath, data,true);

            return true;
        }

        public async UniTask LoadAllMonster()
        {
            List<MonsterIdelData> dataList = monsterCollection.monsterUnitList;
            
            for (int i = 0; i < dataList.Count; i++)
            {
                GameObject target = await SpawnMonsterInstance("Assets/Prefabs/Monsters/Monster.prefab", dataList[i], false);
            }
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth * 0.25f, Camera.main.pixelHeight * 0.5f)), spawnRadious);
        }

        [ContextMenu("Save")]
        public void Save()
        {
            monsterCollection.SaveData();
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            monsterCollection.Clear();
            monsterCollection.SaveData();
        }
    }
}