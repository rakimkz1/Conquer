using UnityEngine;
using UnityEngine.UI;

namespace BattleField
{
    public class Base_View : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        private float _maxHealth;
        private float _health;

        private void Start()
        {
            Base _base = GetComponent<Base>();
            _base._healthHandler.OnDamage += OnTakeDamage;
            _maxHealth = _base._healthHandler.maxHealth;
            _health = _base._healthHandler.health;
        }

        private void OnTakeDamage(float damage)
        {
            ShowGatesHealth();
        }

        private void ShowGatesHealth()
        {
            _healthBar.fillAmount = _health / _maxHealth;
        }
    }
}
