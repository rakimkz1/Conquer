
using Assets._Scripts.Managers;
using Zenject;

namespace MainHUB.Extractor
{
    public class SpawnPanel_ViewModel : ViewModel<SpawnPanel_Model>
    {
        private ManaManager manaManager;
        public int cost;

        public SpawnPanel_ViewModel(SpawnPanel_Model model, ManaManager mana) : base(model) 
        {
            manaManager = mana;
        }

        protected override void OnInitialize()
        {
            cost = model.manaCost;
        }

        public bool SpawnMonster()
        {
            bool isAffordable = manaManager.SpawnMonster(model.manaCost, model.prefabKey);
            return isAffordable;
        }

        public class Factory : IFactory<SpawnPanel_Model ,SpawnPanel_ViewModel>
        {

            public SpawnPanel_ViewModel Create(SpawnPanel_Model model)
            {
                return new SpawnPanel_ViewModel(model, ManaManager.Instance);
            }
        }
    }
}