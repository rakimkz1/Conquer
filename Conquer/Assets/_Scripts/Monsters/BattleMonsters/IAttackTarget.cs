using System;
using UnityEngine;

namespace Monsters
{
    public interface IAttackTarget
    {
        float attackPriority { get; set; }
        Vector3 targetPosition { get; set; }
        event Action OnDead;
    }
}