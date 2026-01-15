using System.Collections.Generic;
using UnityEngine;

namespace BattleField
{
    public class ProjectileViewManager
    {
        private ProjectilesPresets so_presets;
        private ResourceManager _resourceManager;
        private Queue<ArrowViewProjectile> arrowPool = new Queue<ArrowViewProjectile>();
        private Queue<MagicViewProjectile> magicPool = new Queue<MagicViewProjectile>();
        private Queue<SiegeViewProjectile> siegePool = new Queue<SiegeViewProjectile>();
        private GameObject _arrowPrefab;
        private GameObject _magicPrefab;
        private GameObject _siegePrefab;
        public ProjectileViewManager(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            resourceManager.LoadAsset<ProjectilesPresets>("Assets/Data/ScriptableObject/ProjectilePresets.asset", asset =>
            {
                so_presets = asset;
            });
            resourceManager.LoadAsset<GameObject>("Assets/Prefabs/Monsters/Projectiles/ArrowView.prefab", prefab =>
            {
                _arrowPrefab = prefab;
            });
            resourceManager.LoadAsset<GameObject>("Assets/Prefabs/Monsters/Projectiles/Magic.prefab", prefab =>
            {
                _magicPrefab = prefab;
            });
            resourceManager.LoadAsset<GameObject>("Assets/Prefabs/Monsters/Projectiles/Siege.prefab", prefab =>
            {
                _siegePrefab = prefab;
            });
        }

        public void ShootTargetProjectile(Vector3 initialPos, Transform target, float flyingTime, MonsterIdelData monsterData, bool isEnemy)
        {
            string path = so_presets.GetPreset(monsterData, isEnemy);
            _resourceManager.LoadAsset<Sprite>(path, sprite =>
            {
                if (arrowPool.Count == 0)
                {
                    GameObject arrow = GameObject.Instantiate(_arrowPrefab);
                    arrow.GetComponent<ArrowViewProjectile>().OnArrowHit += AddArrowPool;
                    arrow.GetComponent<ArrowViewProjectile>().ShootArrow(initialPos, target, flyingTime, sprite);
                }
                else
                {
                    ArrowViewProjectile arrow = arrowPool.Dequeue();
                    arrow.ShootArrow(initialPos, target, flyingTime, sprite);
                }
            });
        }

        private void AddArrowPool(ArrowViewProjectile projectile)
        {
            arrowPool.Enqueue(projectile);
        }

        public void ShootAreaProjectile(Vector3 initialPos, Vector3 endPosition, float flyingTime, MonsterIdelData monsterData, bool isEnemy)
        {
            string path = so_presets.GetPreset(monsterData, isEnemy);
            _resourceManager.LoadAsset<Sprite>(path, sprite =>
            {
                if(magicPool.Count == 0)
                {
                    GameObject magic = GameObject.Instantiate(_magicPrefab);
                    magic.GetComponent<MagicViewProjectile>().OnMagicHit += AddMagicPool;
                    magic.GetComponent<MagicViewProjectile>().CastMagic(initialPos, endPosition, flyingTime, sprite);
                }
                else
                {
                    MagicViewProjectile magic = magicPool.Dequeue();
                    magic.CastMagic(initialPos, endPosition, flyingTime, sprite);
                }
            });
        }

        private void AddMagicPool(MagicViewProjectile projectile)
        {
            magicPool.Enqueue(projectile);
        }
        public void ShootSiegeProjectile(Vector3 initialPos, Transform target, float flyingTime, MonsterIdelData monsterData, bool isEnemy)
        {
            string path = so_presets.GetPreset(monsterData, isEnemy);
            _resourceManager.LoadAsset<Sprite>(path, sprite =>
            {
                if (siegePool.Count == 0)
                {
                    GameObject siege = GameObject.Instantiate(_siegePrefab);
                    siege.GetComponent<SiegeViewProjectile>().OnSiegeHit += AddSiegePool;
                    siege.GetComponent<SiegeViewProjectile>().ShootSiege(initialPos, target, flyingTime, sprite);
                }
                else
                {
                    SiegeViewProjectile siege = siegePool.Dequeue();
                    siege.ShootSiege(initialPos, target, flyingTime, sprite);
                }
            });
        }

        private void AddSiegePool(SiegeViewProjectile projectile)
        {
            siegePool.Enqueue(projectile);
        }

        public void ShootBaseArcherProjectile(Vector3 initialPos, Transform target, float flyingTime, bool isEnemy)
        {
            string path = "Assets/Sprites/Monsters/level1/arrow1.B.asset";
            _resourceManager.LoadAsset<Sprite>(path, sprite =>
            {
                if(arrowPool.Count == 0)
                {
                    GameObject arrow = GameObject.Instantiate(_arrowPrefab);
                    arrow.GetComponent<ArrowViewProjectile>().OnArrowHit += AddArrowPool;
                    arrow.GetComponent<ArrowViewProjectile>().ShootArrow(initialPos, target, flyingTime, sprite);
                }
                else
                {
                    ArrowViewProjectile arrow = arrowPool.Dequeue();
                    arrow.ShootArrow(initialPos, target, flyingTime, sprite);
                }
            });
        }
    }
}
