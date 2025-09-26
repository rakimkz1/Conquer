using UnityEngine;
using UnityEngine.UI;

namespace BattleField
{
    public class ExtractorView : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        private Extractor _extractor;
        public void Init(Extractor extractor)
        {
            _extractor = extractor;
            _extractor._healthHandler.OnDamage += TakeDamage;
        }

        private void TakeDamage(float health)
        {
            Debug.Log("takeDamage");
            _healthBar.fillAmount = health / _extractor._healthHandler.maxHealth;
        }
    }
}