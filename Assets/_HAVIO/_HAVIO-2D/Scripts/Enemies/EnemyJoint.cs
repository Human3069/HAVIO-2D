using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HAVIO
{
    [System.Serializable]
    public class EnemyJoint
    {
        private const float FLIP_THRESHOLD = 0.1f;

        private Enemy _enemy;

        [SerializeField]
        private SpriteRenderer[] spriteRenderers;

        private bool isFlippable = true;

        public void Initialize(Enemy enemy)
        {
            this._enemy = enemy;
        }

        public void LookAtPlayer()
        {
            Vector3 playerPos = HelicopterController.Instance.transform.position;
            Vector3 eyePos = _enemy.Data.EyeTransform.position;
            Vector3 playerDirection = (playerPos - eyePos).normalized;

            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                renderer.transform.right = -playerDirection;
            }
        }

        public bool IsFlipToLookAtPlayer()
        {
            Vector3 playerPos = HelicopterController.Instance.transform.position;
            float xDiff = playerPos.x - _enemy.Data.EyeTransform.position.x;

            return xDiff >= 0;
        }

        public bool IsFlipToMoving(Vector3 targetPosition)
        {
            Vector3 currentPosition = _enemy.transform.position;
            Vector3 direction = (targetPosition - currentPosition).normalized;

            return direction.x >= 0;
        }

        public void FlipToMove(bool isReversed)
        {
            if (isReversed == true)
            {
                _enemy.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else
            {
                _enemy.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }

        public void Flip(bool isReversed)
        {
            if (isFlippable == false)
            {
                return;
            }

            if (isReversed == true)
            {
                _enemy.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else
            {
                _enemy.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }

            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                renderer.flipY = isReversed;
            }

            FlipAsync().Forget();
        }

        private async UniTaskVoid FlipAsync()
        {
            isFlippable = false;
            await UniTask.WaitForSeconds(FLIP_THRESHOLD);
            isFlippable = true;
        }
    }
}