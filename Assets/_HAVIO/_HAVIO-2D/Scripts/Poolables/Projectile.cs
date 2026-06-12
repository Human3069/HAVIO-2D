using _KMH_Framework;
using Cysharp.Threading.Tasks;
using HAVIO;
using System.Threading;
using UnityEngine;

public class Projectile : MonoPoolable
{
    [Header("=== Projectile ===")]
    [SerializeField]
    private float velocity = 100f;
    [SerializeField]
    private float effectScaleRatio = 1f;

    private Vector3 currentPoint;
    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        this.rigidbody.linearVelocity = this.transform.right * velocity;
        CheckRaycastAsync(tokenSource.Token).Forget();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        currentPoint = Vector3.zero;
        rigidbody.linearVelocity = Vector2.zero;
    }

    public void OnBeforeEnablePool(Vector3 position, Quaternion rotation)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
    }

    private async UniTaskVoid CheckRaycastAsync(CancellationToken token)
    {
        while (token.IsCancellationRequested == false)
        {
            if (currentPoint != Vector3.zero)
            {
                Vector3 direction = (this.transform.position - currentPoint).normalized;
                float maxDistance = Vector3.Distance(this.transform.position, currentPoint);

                RaycastHit2D[] hits = Physics2D.RaycastAll(currentPoint, direction, maxDistance);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider.TryGetComponent(out Impactable impactable) == true)
                    {
                        impactable.Impact(hit, poolType, effectScaleRatio);

                        rigidbody.linearVelocity = Vector2.zero;
                        this.transform.position = hit.point;

                        await UniTask.Yield();

                        this.gameObject.DisablePool(poolType);
                        return;
                    }
                }
            }

            currentPoint = this.transform.position;

            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: token);
        }
    }
}
