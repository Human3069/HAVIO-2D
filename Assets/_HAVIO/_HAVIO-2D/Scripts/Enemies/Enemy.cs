using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace HAVIO
{
    public class Enemy : MonoBehaviour
    {
        public EnemyData Data;

        private bool _isMoving;
        public bool IsMoving
        {
            get
            {
                return _isMoving;
            }
            private set
            {
                _isMoving = value;
            }
        }

        private CancellationTokenSource source;

        private void Awake()
        {
            Data.Initialize(this);
            source = new CancellationTokenSource();
        }

        private void OnApplicationQuit()
        {
            source?.Cancel();
        }

        public void Move(Vector3 targetPosition)
        {
            Debug.Log("Move");

            IsMoving = true;
            source?.Cancel();
            source = new CancellationTokenSource();
            
            Data.Animator.PlayMove(true);
            MoveAsync(targetPosition).Forget();
        }

        private async UniTaskVoid MoveAsync(Vector3 targetPosition)
        {
            Vector3 currentPosition = this.transform.position;
            float distance = Vector3.Distance(currentPosition, targetPosition);
            float delta = Data.MoveSpeed / distance;
            float time = 0f;

            while (time < 1f)
            {
                this.transform.position = Vector3.Lerp(currentPosition, targetPosition, time);
                time += (delta * Time.deltaTime);

                await UniTask.Yield(source.Token);
            }

            this.transform.position = targetPosition;

            Data.Animator.PlayMove(false);
            IsMoving = false;
        }

        public void StopMove()
        {
            Debug.Log("Stop Move");

            IsMoving = false;
            source?.Cancel();
            source = new CancellationTokenSource();

            Data.Animator.PlayMove(false);
        }

        public bool IsArrived(Vector3 targetPosition)
        {
            Vector3 currentPosition = this.transform.position;
            float distance = Vector3.Distance(currentPosition, targetPosition);

            return distance <= 0.1f;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(Data.EyeTransform.position, Data.SearchRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Data.EyeTransform.position, Data.AttackRange);
        }
    }
}