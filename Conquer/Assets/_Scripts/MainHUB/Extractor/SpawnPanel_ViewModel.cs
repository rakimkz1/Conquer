
using Assets._Scripts.Managers;
using Zenject;

namespace MainHUB.Extractor
{
    public class SpawnPanel_ViewModel : ViewModel<SpawnPanel_Model>
    {
        private ManaManager manaManager;
        public int cost;

        public SpawnPanel_ViewModel(SpawnPanel_Model model, ManaManager manaManager) : base(model)
        {
            this.manaManager = manaManager;
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

        public class Factory : IFactory<SpawnPanel_ViewModel>
        {
            private readonly SpawnPanel_Model model;
            private readonly ManaManager mana;

            public Factory(SpawnPanel_Model model, ManaManager mana)
            {
                this.model = model;
                this.mana = mana;
            }

            public SpawnPanel_ViewModel Create()
            {
                return new SpawnPanel_ViewModel(model, mana);
            }
        }
    }
}