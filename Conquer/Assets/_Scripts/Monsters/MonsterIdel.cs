using Cysharp.Threading.Tasks;
using Monsters.MonsterState;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Monsters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MonsterIdel : MonoBehaviour
    {
        public IdelStateBase waitState;
        public IdelStateBase moveState;
        public IdelStateBase dragState;

        public Rigidbody2D rb;
        protected IdelStateBase currentState;

        private CancellationTokenSource _cancelToken;
        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
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

        private void CheckDraging()
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            bool isMousePointing = (hit.collider != null) & (hit.collider?.GetComponent<MonsterIdel>() == this);
            if (!isMousePointing)
                return;
            if (Input.GetMouseButtonDown(0) && currentState.GetType() != typeof(DraggingIdelState)) 
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
            _cancelToken?.Cancel();
            SwichState(dragState);
        }

        public void OnDragging()
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = pos;
        }

        private void OnEndDrag()
        {
            currentState.OnExit();
            currentState = null;
        }

        public void TransmisionHandle()
        {
            CheckDraging();
            if (currentState == null)
                SwichState(waitState);
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
            SwichState(toState);
        }
        public void WanderToDiraction(float speed, Vector2 diraction)
        {
            rb.linearVelocity = diraction * speed;
        }
    }
}