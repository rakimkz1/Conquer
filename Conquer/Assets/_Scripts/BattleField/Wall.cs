using BattleField;
using Monsters;
using System;
using UnityEngine;
using Zenject;

public class Wall : MonoBehaviour, IAttackTarget
{
    public bool isEnemyWall;
    public float attackPriority { get; set; }
    public Vector3 targetPosition { get; set; }
    public event Action OnDead;

    private AttackableUnitsOnSceneCollection _targetCollection;

    [Inject]
    private void Construct(AttackableUnitsOnSceneCollection targetCollection)
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

        targetPosition = transform.position;
        attackPriority = 1f;
    }
}
