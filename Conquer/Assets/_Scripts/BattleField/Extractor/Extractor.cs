using Monsters;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace BattleField
{
    public class Extractor : MonoBehaviour, IAttackTarget, IPointerClickHandler
    {
        private ManaHandler _manaHandler;
        private AttackableUnitsOnSceneCollection _targetCollection;
        private ExtractorHealthHandler _healthHandler;
        private ExtractorManaProducer _manaProducer;
        private BattleStarter _battleStarter;
        public event Action<IAttackTarget> OnDead;

        public float attackPriority { get; set; }
        public Vector3 targetPosition { get; set; }

        [Inject]
        private void Construct(ManaHandler manaHandler, AttackableUnitsOnSceneCollection targetCollection, BattleStarter battleStarter)
        {
            _manaHandler = manaHandler;
            _targetCollection = targetCollection;
            _battleStarter = battleStarter;
            Init();
        }

        private void Init()
        {
            _targetCollection.AddPlayerUnit(this);
        }

        public void SetProperties(float manaPerPeriod, float manaPerClick, float periodTime, float maxHealth, float repairmentAmount)
        {
            _healthHandler = new ExtractorHealthHandler(maxHealth, repairmentAmount);
            _healthHandler.OnDead += ()=> OnDead?.Invoke(this);
            _manaProducer = new ExtractorManaProducer(manaPerPeriod, manaPerClick, periodTime, _manaHandler);
            _battleStarter.OnBattleStart += _manaProducer.StartProduceMana;
        }
        public void TakeDamage(float damage) => _healthHandler.TakeDamage(damage);

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Clicked E");
            if(eventData.pointerCurrentRaycast.gameObject.GetComponent<Extractor>() == this)
                Clicked();
        }

        private void Clicked()
        {
            Debug.Log("Clicked");
            if (_healthHandler.isWorking)
                _manaProducer.ProduceManaClick();
            else
                _healthHandler.Repair();
        }
    }
}