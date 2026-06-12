using _KMH_Framework;
using _KMH_Framework.Pool;
using UnityEngine;

public enum ImpactType
{
    None = -1,

    Default = 100,
    Flesh = 110,
}

public class Impactable : MonoBehaviour
{
    [SerializeField]
    private ImpactType impactType = ImpactType.None;
    public float PenetrateLevel = 1f;

    public void Impact(RaycastHit2D hit, PoolType projectileType, float ratio)
    {
        PoolType poolType = impactType.GetPoolType(projectileType);
        poolType.EnablePool(OnBeforeImpactEnablePool);
        void OnBeforeImpactEnablePool(GameObject obj)
        {
            obj.transform.position = hit.point;
            obj.transform.up = hit.normal;
            obj.transform.localScale = Vector3.one * ratio;
        }
    }
}
