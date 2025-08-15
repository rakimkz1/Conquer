using System;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    [RequireComponent(typeof(Button))]
    public class LevelPoint : MonoBehaviour
    {
        public bool isBlocked;
        [Range(0, 3)] public int starNumber;
        public int levelNumber;
        public event Action<int> OnLevelSelected;

        private Button _button;
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() =>
            {
                if(!isBlocked)
                    OnLevelSelected?.Invoke(levelNumber);
            });
        }
        public void SetData(LevelData data)
        {
            isBlocked = data.isBlocked;
            starNumber = data.starNumber;
            levelNumber = data.levelOrder;
        }
        public LevelData GetData() => new LevelData(levelNumber, isBlocked, starNumber);
    }
}