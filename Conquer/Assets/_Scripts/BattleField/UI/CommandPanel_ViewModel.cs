using Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace BattleField {
    public class CommandPanel_ViewModel : ViewModel<CommandPanel_Model>
    {
        public bool[] settedTypeToCommand = new bool[Enum.GetValues(typeof(MonsterType)).Length];
        private ArmyCommandHandler _commandHandler;
        public event Action<bool> OnShowTankUnit;
        public event Action<bool> OnShowSprinterUnit;
        public event Action<bool> OnShowRangerUnit;
        public event Action<bool> OnShowMageUnit;
        public event Action<bool> OnShowSiegeUnit;
        public CommandPanel_ViewModel(CommandPanel_Model model, ArmyCommandHandler commandHandler) : base(model)
        {
            _commandHandler = commandHandler;
        }

        public void OnPressedUnitType(MonsterType type)
        {
            settedTypeToCommand[(int)type] = !settedTypeToCommand[(int)type];

            if (type == MonsterType.Tanks)
                OnShowTankUnit?.Invoke(settedTypeToCommand[(int)type]);
            else if (type == MonsterType.Sprinter)
                OnShowSprinterUnit?.Invoke(settedTypeToCommand[(int)type]);
            else if (type == MonsterType.Rangers)
                OnShowRangerUnit?.Invoke(settedTypeToCommand[(int)type]);
            else if (type == MonsterType.Mage)
                OnShowMageUnit?.Invoke(settedTypeToCommand[(int)type]);
            else
                OnShowSiegeUnit?.Invoke(settedTypeToCommand[(int)type]);
        }

        public void SayCommand(ArmyCommandTypes commandType)
        {
            for(int  i = 0; i < settedTypeToCommand.Length; i++)
            {
                if (!settedTypeToCommand[i] || !_commandHandler.OnCommand.ContainsKey((MonsterType)i))
                    continue;
                _commandHandler.OnCommand[(MonsterType)i]?.Invoke(commandType);
            }
        }
    }
}