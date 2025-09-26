using UnityEngine;
using UnityEngine.UI;

namespace BattleField
{
    public class EnemyExtractorView : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        private EnemyExtractor enemyExtractor;

        public void Init(EnemyExtractor enemyExtractor)
        {
            this.enemyExtractor = enemyExtractor;
            this.enemyExtractor.OnDamage += TakeDamage;
        }

        private void TakeDamage(float health)
        {
            _healthBar.fillAmount = health / enemyExtractor.maxHealth;
        }
    }
}