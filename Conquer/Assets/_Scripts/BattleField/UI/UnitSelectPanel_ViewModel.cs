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
        public event Action OnGameStarted;

        private LevelBuilder _levelBuilder;
        public UnitSelectPanel_ViewModel(UnitSelectPanel_Model model, LevelBuilder levelBuilder) : base(model)
        {
            _levelBuilder = levelBuilder;
            Initialize();
        }

        public void PressStartBattleButton()
        {
            _levelBuilder.SetAllMonsterUnit(selectedMonsters.ToList());
            HideWholeSelectionPanel?.Invoke();
            OnGameStarted?.Invoke();
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
