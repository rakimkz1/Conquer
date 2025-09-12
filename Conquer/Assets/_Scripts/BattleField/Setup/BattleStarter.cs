using System;

namespace BattleField
{
    public class BattleStarter
    {
        public Action OnBattleStart;
        public void StartBattle() => OnBattleStart?.Invoke();
    }
}
