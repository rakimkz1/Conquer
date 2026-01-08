using BattleField;
using Monsters;
using System;
using UnityEngine;
using Zenject;

public class Wall : MonoBehaviour, IAttackTarget
{
    public bool isEnemyWall;
    public float attackPriority { get; set; }
    public Transform targetPosition { get; set; }
    public float powerScale { get; set; }
    public bool isDead { get; set; }

    public event Action<IAttackTarget> OnDead;
    public event Action<IAttackTarget> OnExitTargetCollection;

    private AttackableCollection _targetCollection;

    [Inject]
    private void Construct(AttackableCollection targetCollection)
    {
        _targetCollection = targetCollection;
        Init();
    }

    private void Init()
    {
        if (isEnemyWall)
            _targetCollection.AddEnemyUnit(this);
        else
            _targetCollection.AddPlayerUnit(this);

        targetPosition = transform;
        attackPriority = 1f;
    }
    private void Update()
    {
        targetPosition = transform;
    }

    public void TakeDamage(float damage)
    {
    }
}
