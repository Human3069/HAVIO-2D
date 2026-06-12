using _KMH_Framework;
using _KMH_Framework.Pool;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace HAVIO
{
    public class MonoPoolable : MonoBehaviour
    {
        [Header("=== Poolable ===")]
        [SerializeField]
        protected PoolType poolType;
        [SerializeField]
        protected float lifeTime;

        protected CancellationTokenSource tokenSource;

        protected virtual void OnEnable()
        {
            tokenSource = new CancellationTokenSource();
            CheckLifetimeAsync(tokenSource.Token).Forget();
        }

        protected virtual void OnDisable()
        {
            tokenSource?.Cancel();
        }

        protected virtual async UniTaskVoid CheckLifetimeAsync(CancellationToken token)
        {
            await UniTask.WaitForSeconds(lifeTime, cancellationToken: token);
            this.gameObject.DisablePool(poolType);
        }
    }
}