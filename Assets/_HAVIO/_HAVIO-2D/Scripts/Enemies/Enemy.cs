using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace HAVIO
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private EnemyData data;

        private CancellationTokenSource moveSource;

        private void Awake()
        {
            data.Initialize(this);
            moveSource = new CancellationTokenSource();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            Gizmos.DrawWireSphere(data.EyeTransform.position, data.AttackPlayerDistance);

            Gizmos.color = new Color(1f, 1f, 0f, 0.5f);
            Gizmos.DrawWireSphere(data.EyeTransform.position, data.FindPlayerDistance);
        }

        public void Move(Vector3 position)
        {
            moveSource?.Cancel();
            moveSource = new CancellationTokenSource();
            MoveAsync(position, moveSource.Token).Forget();
        }

        private async UniTaskVoid MoveAsync(Vector3 position, CancellationToken token)
        {
            Vector3 direction = (position - data.Transform.position).normalized;
            data.Animator.PlayMove(true);
    
            while (data.IsInRange(position, 0.1f) == false)
            {
                this.transform.Translate(direction * data.MoveSpeed * Time.deltaTime);
                await UniTask.Yield(token);
            }

            data.Animator.PlayMove(false);
        }

        public void StopMove()
        {
            moveSource?.Cancel();
            data.Animator.PlayMove(false);
        }
    }
}