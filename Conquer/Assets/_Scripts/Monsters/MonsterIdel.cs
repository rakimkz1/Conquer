using Cysharp.Threading.Tasks;
using MainHUB.HUB_Managers;
using Monsters.MonsterState;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Monsters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MonsterIdel : MonoBehaviour
    {
        public IdelStateBase waitState;
        public IdelStateBase moveState;
        public IdelStateBase dragState;
        public Rigidbody2D rb;
        public MonsterType monsterType;
        public int monsterLevel;

        protected IdelStateBase currentState;

        private CancellationTokenSource _cancelToken;
        private MonsterSpawnManager _monsterSpawn;
        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        [Inject]
        public void Construct(MonsterSpawnManager monsterSpawn)
        {
            _monsterSpawn = monsterSpawn;
        }

        private void Update()
        {
            TransmisionHandle();
            ActionStateHandle();
        }

        private void ActionStateHandle()
        {
            currentState?.OnWork();
        }

        public void SetMonsterData(MonsterIdelData data)
        {
            monsterLevel = data.monsterLevel;
            monsterType = data.monsterType;
        }

        private void CheckDraging()
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            bool isMousePointing = (hit.collider != null) & (hit.collider?.GetComponent<MonsterIdel>() == this);

            if (Input.GetMouseButtonDown(0) && currentState.GetType() != typeof(DraggingIdelState) && isMousePointing) 
                OnBeginDrag();
            if (Input.GetMouseButtonUp(0) && currentState.GetType() == typeof(DraggingIdelState))
                OnEndDrag();
        }

        private void SwichState(IdelStateBase toState)
        {
            currentState?.OnExit();
            currentState = Instantiate(toState);
            currentState.OnEnter(this);
        }

        private void OnBeginDrag()
        {
            CancelUniTask();
            SwichState(dragState);
            Collider2D collider = gameObject.GetComponent<Collider2D>();

            collider.isTrigger = true;
        }

        public void OnDragging()
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = pos;
        }

        private void CheckIsUnity()
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.zero);

            for(int i = 0;i < hit.Length; i++)
            {
                MonsterIdel target = hit[i].collider.gameObject.GetComponent<MonsterIdel>();
                if (target != null && target != this && target.monsterType == monsterType && target.monsterLevel == monsterLevel)
                    UnityMonster(target);
            }
        }

        private void UnityMonster(MonsterIdel target)
        {
            monsterLevel++;
            _monsterSpawn.UnityTwoMonsters(this, target);
        }

        private void OnEndDrag()
        {
            CheckIsUnity();
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            currentState.OnExit();
            currentState = null;
        }

        public void TransmisionHandle()
        {
            if (currentState == null)
                SwichState(waitState);
            CheckDraging();
        }

        public async UniTask SwichStateByTime(float time, IdelStateBase toState)
        {
            _cancelToken = new CancellationTokenSource();
            try
            {
                await UniTask.WaitForSeconds(time, cancellationToken: _cancelToken.Token);
            }
            catch 
            {
                return;
            }
            if(gameObject != null)
                SwichState(toState);
        }
        public void CancelUniTask()
        {
                _cancelToken?.Cancel();
        }
        public void WanderToDiraction(float speed, Vector2 diraction)
        {
            rb.linearVelocity = diraction * speed;
        }

        private void OnDestroy()
        {
            Destroy(currentState);
        }
    }
}