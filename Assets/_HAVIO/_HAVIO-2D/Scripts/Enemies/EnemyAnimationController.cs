using UnityEngine;

namespace HAVIO
{
    public class EnemyAnimationController
    {
        private Animator _animator;
        
        public EnemyAnimationController(Enemy enemy)
        {
            this._animator = enemy.GetComponent<Animator>();
        }
        
        public void PlayMove(bool isMoving)
        {
            _animator.SetBool("IsMoving", isMoving);
            _animator.SetTrigger("IsMovingStateChanged");
        }
    }
}