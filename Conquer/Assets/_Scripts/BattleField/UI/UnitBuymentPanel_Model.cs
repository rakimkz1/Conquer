using UniRx;

namespace BattleField
{
    public class UnitBuymentPanel_Model : Model
    {
        public ReactiveProperty<int> unitCost = new ReactiveProperty<int>();
        private BattleSceneSetting _battleSettings;
        public UnitBuymentPanel_Model(BattleSceneSetting battleSettings)
        {
            _battleSettings = battleSettings;
            Init();
        }

        private void Init()
        {
            unitCost.Value = _battleSettings.unitManaCost;
        }
    }
}