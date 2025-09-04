using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

namespace BattleField
{
    public class EnemyWaveHandler
    {

        public event Action onRoundOver;
        private UnitSelectPanel_ViewModel _selectionViewModel;
        private LevelBuilder _levelBuilder;
        private CancellationTokenSource _cancel;
        private BattleSceneSetting _sceneSettings;
        private MonsterUnitFactory _factory;
        private BattleSceneSetting.EnemyWave _currentWave;
        private int waveNumber = 0;
        public EnemyWaveHandler(UnitSelectPanel_ViewModel selectionViewModel, LevelBuilder levelBuilder, MonsterUnitFactory factory)
        {
            _selectionViewModel = selectionViewModel;
            _levelBuilder = levelBuilder;
            _factory = factory;
            Init();
        }

        private void Init()
        {
            _selectionViewModel.OnGameStarted += StartWave;
        }

        public void StartWave()
        {
            _sceneSettings = _levelBuilder.CurrentSceneSettings;
            _currentWave = _sceneSettings.enemyWaves[waveNumber];
            WaitWaveDuraction();
            SpawnAllEnemyUnit();
        }

        private async UniTask WaitWaveDuraction()
        {
            _cancel = new CancellationTokenSource();
            try
            {
                await UniTask.Delay((int)(_currentWave.waveDuration * 1000f),cancellationToken: _cancel.Token);
            }
            catch { return; }

            MoveToNextWave();
        }

        private void MoveToNextWave()
        {
            waveNumber++;
            if(waveNumber == _sceneSettings.enemyWaves.Count)
            {
                onRoundOver?.Invoke();
                return;
            }
            _currentWave = _sceneSettings.enemyWaves[waveNumber];
            WaitWaveDuraction();
            SpawnAllEnemyUnit();
        }

        private async UniTask SpawnAllEnemyUnit()
        {
            int ploteNumber = _currentWave.enemyWaves.Count;
            for (int i = 0; i < ploteNumber; i++)
            {
                int unitNumber = _currentWave.enemyWaves[i].unitNumber;
                for(int j = 0; j< unitNumber; j++)
                {
                    MonsterIdelData data = new MonsterIdelData(_currentWave.enemyWaves[i].level ,_currentWave.enemyWaves[i].monsterType);
                    _factory.Create(true, data, Vector3.zero);
                    await UniTask.Delay(200);
                }
                await UniTask.Delay(1000);
            }
        }

        public void StopWave()
        {
            _cancel?.Cancel();
        }
    }
}