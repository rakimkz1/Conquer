using MainHUB.HUB_Managers;
using Monsters.IdelMonster;
using Monsters.MonsterState;
using System;
using UnityEngine;
using Zenject;

namespace Monsters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MonsterIdel : MonoBehaviour
    {
        public Rigidbody2D rb;
        public MonsterType monsterType;
        public int monsterLevel;
        public IdelStateBase waitState;
        public IdelStateBase moveState;
        public IdelStateBase dragState;
        public DragAndDropHandler dragAndDropHandler;
        public MonsterIdelStateHandler stateHandler;
        public MonsterUniteHandler uniteHandler;
        public MonsterMovementHandler movementHandler;
        public MonsterSpawnManager monsterSpawn;
        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        [Inject]
        public void Construct(MonsterSpawnManager monsterSpawn)
        {
            this.monsterSpawn = monsterSpawn;
            Init();
        }

        private void Init()
        {
            dragAndDropHandler = new DragAndDropHandler(this);
            stateHandler = new MonsterIdelStateHandler(this, waitState, moveState, dragState);
            uniteHandler = new MonsterUniteHandler(this);
            movementHandler = new MonsterMovementHandler(this);
            Debug.Log("Init");
        }

        private void Update()
        {
            StateUpdate();
        }

        private void StateUpdate()
        {
            stateHandler?.TransmisionHandle();
            stateHandler.currentState?.OnWork();
        }

        public void SetMonsterData(MonsterIdelData data)
        {
            monsterLevel = data.monsterLevel;
            monsterType = data.monsterType;
        }
        public void MoveDirection(Vector2 force)
        {
            rb.linearVelocity = force;
        }

        private void OnDestroy()
        {
            Destroy(stateHandler.currentState);
        }

        public void CancelStateTimer() => stateHandler.CancelUniTask();
    }
}