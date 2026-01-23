using MainHUB.HUB_Managers;
using Zenject;

namespace MainHUB.Extractor
{
    public class SpawnPanel_ViewModel : ViewModel<SpawnPanel_Model>
    {
        private MonsterSpawnManager _monsterSpawn;
        public int cost;

        public SpawnPanel_ViewModel(SpawnPanel_Model model, MonsterSpawnManager monsterSpawn) : base(model) 
        {
            _monsterSpawn = monsterSpawn;
        }

        protected override void OnInitialize()
        {
            cost = model.manaCost;
        }

        public bool SpawnMonster()
        {
            int randomIndex = UnityEngine.Random.Range(0, model.data.Length);
            bool isAffordable = _monsterSpawn.SpawnMonster(model.manaCost, model.prefabPath, model.data[randomIndex]);
            return isAffordable;
        }

        public class Factory : IFactory<SpawnPanel_Model ,SpawnPanel_ViewModel>
        {
            private readonly MonsterSpawnManager monsterSpawn;

            public Factory(MonsterSpawnManager monsterSpawn)
            {
                this.monsterSpawn = monsterSpawn;
            }

            public SpawnPanel_ViewModel Create(SpawnPanel_Model model)
            {
                return new SpawnPanel_ViewModel(model, monsterSpawn);
            }
        }
    }
}