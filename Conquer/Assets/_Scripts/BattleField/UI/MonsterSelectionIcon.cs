using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleField
{
    [RequireComponent(typeof(Button))]
    public class MonsterSelectionIcon : MonoBehaviour
    {
        public event Action<MonsterSelectionIcon> OnPressed;
        public bool isSelected;
        public MonsterIdelData MonsterType {  get; private set; }

        [SerializeField] private TextMeshProUGUI txt_MonterTypeIndicator;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() =>
            {
                isSelected = !isSelected;
                OnPressed?.Invoke(this);
            });
        }
        public void Init(MonsterIdelData monsterType)
        {
            MonsterType = monsterType;
            SetIconImage();
        }

        private void SetIconImage()
        {
            txt_MonterTypeIndicator.text = $"{MonsterType.monsterType}  {MonsterType.monsterLevel}";
        }
        private void OnDestroy()
        {
            OnPressed = null;
        }
    }
}