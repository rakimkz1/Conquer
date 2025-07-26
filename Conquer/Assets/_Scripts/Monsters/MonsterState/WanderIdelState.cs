using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Monsters.MonsterState
{
    [CreateAssetMenu(fileName = "WanderState", menuName = "ScriptableObjects/Monster/States/WanderIdel")]
    public class WanderIdelState : IdelStateBase
    {
        [Range(0f, 5f)] public float minMovingTime;
        [Range(0f, 5f)] public float maxMovingTime;
        public float speed;

        private MonsterIdel target;
        private Vector2 diraction;
        private void OnValidate()
        {
            maxMovingTime = Mathf.Max(maxMovingTime, minMovingTime);
        }

        public override void OnEnter(MonsterIdel target) 
        {
            this.target = target;
            
            float time = Random.Range(minMovingTime, maxMovingTime);
            target.SwichStateByTime(time, target.waitState);
            float angle = Random.Range(0f, 360f);
            diraction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        public override void OnExit()
        {
            target.WanderToDiraction(0f, diraction);
        }

        public override void OnWork()
        {
            target.WanderToDiraction(speed, diraction);
        }
    }
}