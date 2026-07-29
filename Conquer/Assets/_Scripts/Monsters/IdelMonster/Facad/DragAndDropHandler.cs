using Monsters.MonsterState;
using UnityEngine;
namespace Monsters.IdelMonster
{
    public class DragAndDropHandler
    {
        private MonsterIdel _targetMonster;
        public DragAndDropHandler(MonsterIdel targetMonster)
        {
            _targetMonster = targetMonster;
        }

        public void CheckDraging()
        {

            if (_targetMonster.isEgg)
                return;

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            bool isMousePointing = (hit.collider != null) & (hit.collider?.GetComponent<MonsterIdel>() == _targetMonster);

            if (Input.GetMouseButtonDown(0) && _targetMonster.stateHandler.currentState != null && _targetMonster.stateHandler.currentState.GetType() != typeof(DraggingIdelState) && isMousePointing)
                OnBeginDrag();
            if (Input.GetMouseButtonUp(0) && _targetMonster.stateHandler.currentState != null && _targetMonster.stateHandler.currentState.GetType() == typeof(DraggingIdelState))
                OnEndDrag();
        }
        public void OnBeginDrag()
        {
            _targetMonster.stateHandler.CancelUniTask();
            _targetMonster.stateHandler.SwichState(_targetMonster.stateHandler.dragState);
            Collider2D collider = _targetMonster.GetComponent<Collider2D>();

            collider.isTrigger = true;
        }
        public void OnDragging()
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _targetMonster.transform.position = pos;
        }
        public void OnEndDrag()
        {
            _targetMonster.uniteHandler.CheckIsUnity();
            _targetMonster.gameObject.GetComponent<Collider2D>().isTrigger = false;
            _targetMonster.stateHandler.SwichState(_targetMonster.stateHandler.waitState);
        }
    }
}
