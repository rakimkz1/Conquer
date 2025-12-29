using System;
using UnityEngine;

namespace Monsters
{
    public class BattleMonsterAnimationManager
    {
        public event Action OnAttackPreparationEnd;

        private Animator _anim;

        public BattleMonsterAnimationManager(Animator animator)
        {
            _anim = animator;
        }
        public void MoveAnimation()
        {
            _anim.SetBool("isMoving", true);
            _anim.SetBool("isIdel", false);
        }
        public void IdelAnimation()
        {

            _anim.SetBool("isMoving", false);
            _anim.SetBool("isIdel", true);
        }
        public void AttackAnimation()
        {
            _anim.SetTrigger("Attack");
        }
    }
}