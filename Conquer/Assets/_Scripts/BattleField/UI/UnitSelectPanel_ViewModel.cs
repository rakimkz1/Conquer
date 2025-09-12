using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace BattleField
{
    public class UnitSelectPanel_ViewModel : ViewModel<UnitSelectPanel_Model>
    {
        public List<MonsterIdelData> allMonster;
        public ReactiveCollection<MonsterIdelData> notSelectedMonsters = new ReactiveCollection<MonsterIdelData>();
        public ReactiveCollection<MonsterIdelData> selectedMonsters = new ReactiveCollection<MonsterIdelData>();
        public event Action OnShowStartBattleButton;
        public event Action OnHideStartBatlleButton;
        public event Action HideWholeSelectionPanel;

        private LevelBuilder _levelBuilder;
        private BattleStarter _battleStarter;
        public UnitSelectPanel_ViewModel(UnitSelectPanel_Model model, LevelBuilder levelBuilder, BattleStarter battleStarter) : base(model)
        {
            _levelBuilder = levelBuilder;
            Initialize();
            _battleStarter = battleStarter;
        }

        public void PressStartBattleButton()
        {
            _levelBuilder.SetAllMonsterUnit(selectedMonsters.ToList());
            HideWholeSelectionPanel?.Invoke();
            _battleStarter.StartBattle();
        }

        private void Initialize()
        {
            allMonster = model.monstersList;
            notSelectedMonsters.AddRange(allMonster);
            selectedMonsters.ObserveAdd().Subscribe(value =>
            {
                OnShowStartBattleButton?.Invoke();
            });
            selectedMonsters.ObserveRemove().Subscribe(value => 
            {
                if (selectedMonsters.Count == 0)
                    OnHideStartBatlleButton?.Invoke();
            });
        }
    }
}
