using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BattleField
{
    public class ExtractorView : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        private Extractor _extractor;
        private Sequence _clickSequence;
        public void Init(Extractor extractor)
        {
            _extractor = extractor;
            _extractor._healthHandler.OnDamage += TakeDamage;
            _extractor.manaProducer.OnManaProduce += OnProduceMana;
        }

        private void TakeDamage(float health)
        {
            _healthBar.fillAmount = health / _extractor._healthHandler.maxHealth;
        }

        public void OnProduceMana(float manaAmount)
        {
            _clickSequence = DOTween.Sequence();
            _clickSequence.Append(transform.DOScaleY(0.17f, 0.12f).From(0.2f).SetEase(Ease.OutExpo));
            _clickSequence.Append(transform.DOScaleY(0.2f, 0.4f).From(0.17f).SetEase(Ease.OutCubic));
        }
    }
}