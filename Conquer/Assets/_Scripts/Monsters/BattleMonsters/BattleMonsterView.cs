using UnityEngine;
using UnityEngine.UI;
using UniRx;
namespace Monsters
{
    public class BattleMonsterView : MonoBehaviour
    {
        [SerializeField] private Image _monsterHealthBar;
        public BattleMonsterAnimationManager animationManager;
        private BattleMonster _battleMonster;
        private void Start()
        {
            _battleMonster = GetComponent<BattleMonster>();
            _battleMonster.healthHandler.unitHealth.Subscribe(ShowUnitHealth);
            SetColor();
        }

        private void SetColor()
        {
            _battleMonster.GetComponent<SpriteRenderer>().color = (_battleMonster.isEnemyUnit) ? Color.red : Color.blue;
        }

        public void ShowUnitHealth(float health)
        {
            _monsterHealthBar.fillAmount = health / _battleMonster.healthHandler.maxHealth;
        }
    }
}