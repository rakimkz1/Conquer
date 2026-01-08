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
            Init();
        }

        private void Init()
        {
            _battleMonster.healthHandler.unitHealth.Subscribe(ShowUnitHealth);
            _battleMonster.movementHandler._diraction.DistinctUntilChanged().Subscribe(ChangeViewDirection);
        }

        public void ChangeViewDirection(Vector3 vector)
        {
            if (vector.x >= 0f)
                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            else if (vector.x < 0f)
                gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        public void ShowUnitHealth(float health)
        {
            _monsterHealthBar.fillAmount = health / _battleMonster.healthHandler.maxHealth;
        }
    }
}