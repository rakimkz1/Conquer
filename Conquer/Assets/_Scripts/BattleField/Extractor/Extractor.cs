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
        private AttackableCollection _targetCollection;
        public ExtractorHealthHandler _healthHandler;
        private ExtractorManaProducer _manaProducer;
        private BattleStarter _battleStarter;
        public event Action<IAttackTarget> OnDead;
        public event Action<IAttackTarget> OnExitTargetCollection;

        public float attackPriority { get; set; }
        public Transform targetPosition { get; set; }
        public float powerScale { get; set; }
        public bool isDead { get; set; }

        [Inject]
        private void Construct(ManaHandler manaHandler, AttackableCollection targetCollection, BattleStarter battleStarter)
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

        public void SetProperties(float manaPerPeriod, float manaPerClick, float periodTime, float maxHealth, float repairmentAmount, float attackPriority)
        {
            _healthHandler = new ExtractorHealthHandler(this,maxHealth, repairmentAmount, _targetCollection);
            _healthHandler.OnDead += () => OnDead?.Invoke(this);
            _manaProducer = new ExtractorManaProducer(manaPerPeriod, manaPerClick, periodTime, _manaHandler);
            this.attackPriority = attackPriority;
            _battleStarter.OnBattleStart += _manaProducer.StartProduceMana;
        }
        public void TakeDamage(float damage) => _healthHandler.TakeDamage(damage);

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.pointerCurrentRaycast.gameObject.GetComponent<Extractor>() == this)
                Clicked();
        }

        private void Clicked()
        {
            if (_healthHandler.isWorking)
                _manaProducer.ProduceManaClick();
            else
                _healthHandler.Repair();
        }
    }
}