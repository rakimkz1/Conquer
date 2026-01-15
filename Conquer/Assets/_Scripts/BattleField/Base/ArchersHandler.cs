using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace BattleField
{
    public class ArchersHandler
    {
        private Vector2 _archerAreaCenter;
        private Vector2 _archerAreaScale;
        private int _maxArcherInRow;
        private float _distanceBetweenRows;
        private bool _isEnemy;
        private AttackableCollection _attackableCollection;
        private BaseArchersPresets so_archerPreset;
        private ProjectileViewManager _projectileViewManager;
        private BattleStarter _battleStarter;
        private List<BaseArcher> archersSquide = new List<BaseArcher>();

        public ArchersHandler(AttackableCollection attackableCollection, BaseArchersPresets archersPresets, BattleStarter battleStarter, ProjectileViewManager projectileManager)
        {
            _attackableCollection = attackableCollection;
            so_archerPreset = archersPresets;
            _battleStarter = battleStarter;
            _projectileViewManager = projectileManager;
        }
        public void Init(int archersNumber,int maxArcherInRow,float distanceBetweenRows, bool isEnemy, Vector3 archerAreaCenter, Vector3 archerAreaScale)
        {
            _maxArcherInRow = maxArcherInRow;
            _distanceBetweenRows = distanceBetweenRows;
            _archerAreaCenter = archerAreaCenter;
            _isEnemy = isEnemy;
            _archerAreaScale = archerAreaScale;
            _battleStarter.OnBattleStart += StartAchersAttack;
            for (int i = 0;i < archersNumber;i++)
            {
                BaseArcher archer = SpawnArcher(i, archersNumber);
                archersSquide.Add(archer);
            }
        }

        private void StartAchersAttack()
        {
            for(int i = 0;i < archersSquide.Count; i++)
            {
                archersSquide[i].StartAttack();
            }
        }

        private void StopArcherAttack()
        {
            for(int i =0; i <  archersSquide.Count; i++)
            {
                archersSquide[i].StopAttack();
            }
        }

        public BaseArcher SpawnArcher(int archerID, int archersNumber)
        {
            float attackColdown = Random.Range(so_archerPreset.attackingSpeed * 0.85f, so_archerPreset.attackingSpeed * 1.2f);
            float attackDistance = Random.Range(so_archerPreset.archerDistance * 0.9f, so_archerPreset.archerDistance * 1.1f);
            Vector3 archerPos = GetArcherPosition(archerID, archersNumber);
            return new BaseArcher(so_archerPreset.arrowDamage, attackColdown, attackDistance,  so_archerPreset.projectileSpeed, GetArcherPosition(archerID, archersNumber), _isEnemy, _attackableCollection, _projectileViewManager);
        }

        private Vector3 GetArcherPosition(int archerID, int archersNumber)
        {
            Vector2 answer;
            answer.x = ((_isEnemy) ? 1f : -1f) * (archerID / _maxArcherInRow * _distanceBetweenRows + ((_archerAreaScale.x / _maxArcherInRow) - _archerAreaScale.x / 2)) + _archerAreaCenter.x;
            float betweenSpace = (archersNumber % _maxArcherInRow != 0 && (archersNumber - 1) / _maxArcherInRow == archerID / _maxArcherInRow) ? _archerAreaScale.y / ((archersNumber + 1) % _maxArcherInRow) : _archerAreaScale.y / _maxArcherInRow;
            answer.y = (-_archerAreaScale.y / 2 + archerID % _maxArcherInRow * betweenSpace + _archerAreaCenter.y) + ((archersNumber % _maxArcherInRow != 0 && (archersNumber - 1) / _maxArcherInRow == archerID / _maxArcherInRow) ? betweenSpace : 0f);
            return answer;
        }
    }
}