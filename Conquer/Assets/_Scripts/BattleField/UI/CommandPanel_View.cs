using BattleField;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BattleField { 
    public class CommandPanel_View : View<CommandPanel_ViewModel>
    {
        [SerializeField] private Button btn_attackCommand;
        [SerializeField] private Button btn_keepPositionCommand;
        [SerializeField] private Button btn_defenceCommand;
        [SerializeField] private Button btn_retreatCommand;
        [SerializeField] private Button btn_TankType;
        [SerializeField] private Button btn_SprinterType;
        [SerializeField] private Button btn_RangerType;
        [SerializeField] private Button btn_MageType;
        [SerializeField] private Button btn_SiegesType;
        private BattleStarter _battleStarter;
        protected override void OnBind() { }
    
        [Inject] 
        private void Construct( CommandPanel_ViewModel viewModel, BattleStarter battleStarter)
        {
            this.viewModel = viewModel;
            _battleStarter = battleStarter;
            Init();
        }

        private void Init()
        {
            btn_attackCommand.onClick.AddListener(() =>
            {
                viewModel.SayCommand(ArmyCommandTypes.Attack);
            });
            btn_keepPositionCommand.onClick.AddListener(() =>
            {
                viewModel.SayCommand(ArmyCommandTypes.KeepPosition);
            });
            btn_defenceCommand.onClick.AddListener(() =>
            {
                viewModel.SayCommand(ArmyCommandTypes.Defence);
            });
            btn_retreatCommand.onClick.AddListener(() =>
            {
                viewModel.SayCommand(ArmyCommandTypes.Retreat);
            });
            btn_TankType.onClick.AddListener(() =>
            {
                viewModel.OnPressedUnitType(Monsters.MonsterType.Tanks);
            });
            btn_SprinterType.onClick.AddListener(() =>
            {
                viewModel.OnPressedUnitType(Monsters.MonsterType.Sprinter);
            });
            btn_RangerType.onClick.AddListener(() =>
            {
                viewModel.OnPressedUnitType(Monsters.MonsterType.Rangers);
            });
            btn_MageType.onClick.AddListener(() =>
            {
                viewModel.OnPressedUnitType(Monsters.MonsterType.Mage);
            });
            btn_SiegesType.onClick.AddListener(() =>
            {
                viewModel.OnPressedUnitType(Monsters.MonsterType.Sieges);
            });
            viewModel.OnShowTankUnit += ShowInActivedTank;
            viewModel.OnShowSprinterUnit += ShowInActiveSprinter;
            viewModel.OnShowRangerUnit += ShowInActiveRanger;
            viewModel.OnShowMageUnit += ShowInActiveMage;
            viewModel.OnShowSiegeUnit += ShowInActiveSieges;
            _battleStarter.OnBattleStart += ShowPanel;
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
        }

        public void ShowInActivedTank(bool isActive) => btn_TankType.gameObject.GetComponent<Image>().color = isActive ? Color.green : Color.red;
        public void ShowInActiveSprinter(bool isActive) => btn_SprinterType.gameObject.GetComponent<Image>().color = isActive ? Color.green : Color.red;
        public void ShowInActiveRanger(bool isActive) => btn_RangerType.gameObject.GetComponent<Image>().color = isActive ? Color.green : Color.red;
        public void ShowInActiveMage(bool isActive) => btn_MageType.gameObject.GetComponent<Image>().color = isActive ? Color.green : Color.red;
        public void ShowInActiveSieges(bool isAcive) => btn_SiegesType.gameObject.GetComponent<Image>().color = isAcive ? Color.green : Color.red;
        private void OnDestroy()
        {
            _battleStarter.OnBattleStart -= ShowPanel;
        }
    }
}