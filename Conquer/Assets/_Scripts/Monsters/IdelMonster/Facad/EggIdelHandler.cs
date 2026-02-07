using System;
using UnityEngine;

namespace Monsters
{
    public class EggIdelHandler
    {
        public int clickNumberToBrock;
        public Action onMonsterRelease;

        private MonsterIdel monsterIdel;
        public EggIdelHandler(MonsterIdel monsterTarget, int clickNumber)
        {
            this.monsterIdel = monsterTarget;
            clickNumberToBrock = clickNumber;
        }

        public void CheckEgg()
        {

            if(!Input.GetMouseButtonDown(0))
                return;
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            bool isMousePointing = (hit.collider != null) & (hit.collider?.GetComponent<MonsterIdel>() == monsterIdel);

            if (isMousePointing)
                CrackEgg();
        }

        private void CrackEgg()
        {
            clickNumberToBrock--;
            if (clickNumberToBrock <= 0)
                RealaseMonster();
        }

        public void RealaseMonster()
        {
            onMonsterRelease?.Invoke();
        }
    }
}