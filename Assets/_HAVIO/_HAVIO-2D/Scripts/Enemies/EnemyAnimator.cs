using UnityEngine;

namespace HAVIO
{
    public class EnemyAnimator
    {
        private Animator animator;
        
        private readonly int isMovingHash = Animator.StringToHash("IsMoving");
        private readonly int isMovingStateChangedHash = Animator.StringToHash("IsMovingStateChanged");

        public EnemyAnimator(Enemy enemy)
        {
            animator = enemy.GetComponent<Animator>();
        }

        public void PlayMove(bool isMove)
        {
            animator.SetBool(isMovingHash, isMove);
            animator.SetTrigger(isMovingStateChangedHash);
        }
    }
}