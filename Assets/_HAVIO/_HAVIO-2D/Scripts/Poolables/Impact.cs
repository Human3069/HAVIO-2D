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

            bool isFlip = Random.Range(0, 2) == 0;
            float flipX = isFlip ? -1f : 1f;
            this.transform.localScale = new Vector3(flipX, 1f, 1f);

            animator.SetTrigger("Impact");
        }
    }
}