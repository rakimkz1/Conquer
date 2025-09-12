using Cysharp.Threading.Tasks;
using System.Threading;

namespace BattleField
{
    public class ExtractorManaProducer
    {
        private float _manaPerPeriod;
        private float _manaPerClick;
        private float _periodTime;
        private ManaHandler _manaHandler;
        private CancellationTokenSource _cancellation;
        public ExtractorManaProducer(float manaPerPeriod, float manaPerClick, float periodTime, ManaHandler manaHandler)
        {
            _manaPerPeriod = manaPerPeriod;
            _manaPerClick = manaPerClick;
            _periodTime = periodTime;
            _manaHandler = manaHandler;
        }
        public void StartProduceMana()
        {
            if (_cancellation == null || _cancellation.IsCancellationRequested)
                ProduceMana();
        }

        public void StopProduceMana()
        {
            _cancellation?.Cancel();
        }

        public void ProduceManaClick()
        {
            _manaHandler.AddMana(_manaPerClick);
        }

        private async UniTask ProduceMana()
        {
            _cancellation = new CancellationTokenSource();
            while (!_cancellation.IsCancellationRequested)
            {
                _manaHandler.AddMana(_manaPerPeriod);
                try
                {
                    await UniTask.Delay((int)(_periodTime * 1000f), cancellationToken: _cancellation.Token);
                }
                catch { }
            }
        }
    }
}