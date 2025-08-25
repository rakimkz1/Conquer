using System;
using System.Collections.Generic;

namespace BattleField
{
    public class UnitSelectPanel_ViewModel : ViewModel<UnitSelectPanel_Model>
    {
        public List<MonsterIdelData> allMonster;
        public List<MonsterIdelData> notSelectedMonsters = new List<MonsterIdelData>();
        public List<MonsterIdelData> selectedMonsters = new List<MonsterIdelData>();

        public UnitSelectPanel_ViewModel(UnitSelectPanel_Model model) : base(model)
        {
            Initialize();
        }

        private void Initialize()
        {
            allMonster = model.monstersList;
            notSelectedMonsters.AddRange(allMonster);
        }
    }
}
