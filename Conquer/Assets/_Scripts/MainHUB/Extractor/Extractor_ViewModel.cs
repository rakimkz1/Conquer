using Assets._Scripts.Managers;
using System;
using System.Diagnostics;
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

        public Extractor_ViewModel(Extractor_Model model,ManaManager mana) : base(model) 
        {
            this.manaManager = mana;
        }

        protected override void OnInitialize()
        {
            manaAmountOnClick = new ReactiveProperty<float>(model.manaAmountOnClick);
            manaAmountOnClick.Subscribe(value => model.manaAmountOnClick = value).AddTo(disposables);
            ManaManager.Instance.manaAmount.Subscribe(value => OnManaChange?.Invoke(value)).AddTo(disposables);
        }

        public void AddMana(float amount)
        {
            ManaManager.Instance.AddMana(amount);
        }

        public class Factory : IFactory<Extractor_Model,Extractor_ViewModel>
        {

            public Extractor_ViewModel Create(Extractor_Model model)
            {
                return new Extractor_ViewModel(model, ManaManager.Instance);
            }
        }
    }
}