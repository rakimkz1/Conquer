using System;
using UnityEngine;

namespace Monsters
{
    public interface IAttackTarget
    {
        float attackPriority { get; set; }
        float powerScale { get; set; }
        Transform targetPosition { get; set; }
        bool isDead { get; set; }
        void TakeDamage(float damage);
        event Action<IAttackTarget> OnDead;
        event Action<IAttackTarget> OnExitTargetCollection;
    }
}