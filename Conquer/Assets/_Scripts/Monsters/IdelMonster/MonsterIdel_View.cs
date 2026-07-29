using UnityEngine;
using Zenject;

namespace Monsters
{
    public class MonsterIdel_View : MonoBehaviour
    {
        [SerializeField] private GameObject eggSprite;
        [SerializeField] private SpriteRenderer monsterSprite;

        private MonsterIdel monsterIdel;
        private ResourceManager _resourceManager;
        private IdelMonsterSpritePreset so_MonsterSpritePreset;
        private IdelMonsterEggSpritePresets so_MonsterEggSpritePreset;

        [Inject]
        public void Construct(ResourceManager resourceManager, IdelMonsterSpritePreset monsterSpritePreset, IdelMonsterEggSpritePresets eggSpritePreset)
        {
            _resourceManager = resourceManager;
            so_MonsterSpritePreset = monsterSpritePreset;
            so_MonsterEggSpritePreset = eggSpritePreset;
        }

        public void Init()
        {
            monsterIdel = GetComponent<MonsterIdel>();
            monsterIdel.eggIdelHandler.onMonsterRelease += ShowMonsterTrueForm;
            Debug.Log(monsterIdel.isEgg);
            if (monsterIdel.isEgg)
                ShowEggSprite();
            else
                ShowMonsterTrueForm();
        }

        private void ShowEggSprite()
        {
            _resourceManager.LoadAsset<Sprite>(so_MonsterEggSpritePreset.EggSpritePresetPaths[monsterIdel.monsterLevel], asset =>
            {
                eggSprite.GetComponent<SpriteRenderer>().sprite = asset;
            });
        }

        public void ShowMonsterTrueForm()
        {
            Debug.Log("TrueFormShown");
            eggSprite.SetActive(false);
            _resourceManager.LoadAsset<Sprite>(so_MonsterSpritePreset.GetSprite(new MonsterIdelData(monsterIdel.monsterLevel, monsterIdel.monsterType)), asset =>
            {
                monsterSprite.sprite = asset;
            });
        }

        private void OnDestroy()
        {
            monsterIdel.eggIdelHandler.onMonsterRelease = null;
        }
    }
}
