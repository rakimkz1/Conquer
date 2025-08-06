using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Monsters.MonsterState
{
    [CreateAssetMenu(fileName = "DragState", menuName = "ScriptableObjects/Monster/States/DragIdel")]
    public class DraggingIdelState : IdelStateBase
    {
        private MonsterIdel target;
        public override void OnEnter(MonsterIdel target)
        {
            this.target = target;
        }

        public override void OnExit()
        {

        }

        public override void OnWork()
        {
            target.OnDragging();
        }
    }
}