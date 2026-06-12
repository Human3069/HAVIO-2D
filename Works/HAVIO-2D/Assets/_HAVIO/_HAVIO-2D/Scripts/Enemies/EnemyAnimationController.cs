using UnityEngine;

namespace HAVIO
{
    public class EnemyAnimationController
    {
        private Animator _animator;
        
        public EnemyAnimationController(Animator animator)
        {
            this._animator = animator;
        }
        
        public void PlayMove(bool isMoving)
        {
            _animator.SetBool("IsMoving", isMoving);
            _animator.SetTrigger("IsMovingStateChanged");
        }
    }
}