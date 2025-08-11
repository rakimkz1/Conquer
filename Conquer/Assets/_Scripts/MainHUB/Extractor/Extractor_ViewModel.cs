using Assets._Scripts.Managers;
using System;
using UniRx;
using Zenject;

namespace MainHUB.Extractor
{
    public class Extractor_ViewModel : ViewModel<Extractor_Model>
    {
        public event Action<float> OnManaChange;

        public ManaManager manaManager;
        public ReactiveProperty<float> manaAmountOnClick;

        public bool isPanelRolledUp = false;

        public Extractor_ViewModel(Extractor_Model model, ManaManager manaManager) : base(model)  
        {
            this.manaManager = manaManager;
        }

        protected override void OnInitialize()
        {
            manaAmountOnClick = new ReactiveProperty<float>(model.manaAmountOnClick);
            manaAmountOnClick.Subscribe(value => model.manaAmountOnClick = value).AddTo(disposables);

            manaManager.manaAmount.Subscribe(value => OnManaChange?.Invoke(value)).AddTo(disposables);
        }

        public void AddMana(float amount)
        {
            manaManager.AddMana(amount);
        }

        public class Factory : IFactory<Extractor_ViewModel>
        {
            private readonly Extractor_Model model;
            private readonly ManaManager mana;

            public Factory(Extractor_Model model, ManaManager mana)
            {
                this.model = model;
                this.mana = mana;
            }

            public Extractor_ViewModel Create()
            {
                return new Extractor_ViewModel(model, mana);
            }
        }
    }
}