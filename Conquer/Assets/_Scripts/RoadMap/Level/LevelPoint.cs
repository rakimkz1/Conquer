using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Level
{
    [RequireComponent(typeof(Button))]
    public class LevelPoint : MonoBehaviour
    {
        public bool isBlocked;
        [Range(0, 3)] public int starNumber;
        public int levelNumber;

        private Button _button;
        private ResourceManager _resourseManager;

        [Inject]
        private void Construct(ResourceManager resourceManager)
        {
            _resourseManager = resourceManager;
        }
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() =>
            {
                if (!isBlocked)
                    SwitchToBattleScene();
            });
        }


        public void SetData(LevelData data)
        {
            isBlocked = data.isBlocked;
            starNumber = data.starNumber;
            levelNumber = data.levelOrder;
        }
        public LevelData GetData() => new LevelData(levelNumber, isBlocked, starNumber);
        private void SwitchToBattleScene()
        {
            throw new NotImplementedException();
        }
    }
}