using Assets._Scripts.Managers;
using System;
using System.Collections;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets._Scripts.MainHUB
{
    public class Extractor_ViewModel : ViewModel<Extractor_Model>
    {
        public event Action<float> OnManaChange;

        [Inject] public ManaManager manaManager;
        public ReactiveProperty<float> manaAmountOnClick;

        public bool isPanelRolledUp = false;
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
    }
}