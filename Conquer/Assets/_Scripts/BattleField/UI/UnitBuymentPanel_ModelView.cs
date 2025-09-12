using UniRx;

namespace BattleField
{
    public class UnitBuymentPanel_ModelView : ViewModel<UnitBuymentPanel_Model>
    {
        public ReactiveProperty<int> unitCost = new ReactiveProperty<int>();
        public ReactiveProperty<float> manaAmount = new ReactiveProperty<float>();
        private ManaHandler _manaHandler;
        private MonsterSpawnHandler _monsterSpawnHandler;
        public UnitBuymentPanel_ModelView(UnitBuymentPanel_Model model, ManaHandler manaHandler, MonsterSpawnHandler monsterSpawnHandler) : base(model)
        {
            _manaHandler = manaHandler;
            _manaHandler.OnValue += value => manaAmount.Value = value;
            model.unitCost.Subscribe(value => unitCost.Value = value);
            _monsterSpawnHandler = monsterSpawnHandler;
        }

        public void OnBuyUnitPress()
        {
            if (_manaHandler.SpendMana((float)unitCost.Value))
                _monsterSpawnHandler.Spawn(false);
        }
    }
}