using UnityEngine;

namespace Monsters.MonsterState
{
    public abstract class IdelStateBase : ScriptableObject
    {
        public abstract void OnEnter(MonsterIdel target);

        public abstract void OnWork();
        public abstract void OnExit();
    }
}