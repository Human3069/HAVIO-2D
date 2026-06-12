using UnityEngine;

namespace HAVIO
{
    public class Impact : MonoPoolable
    {
        private Animator animator;

        private void Awake()
        {
            animator = this.GetComponent<Animator>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            animator.SetTrigger("Impact");
        }
    }
}