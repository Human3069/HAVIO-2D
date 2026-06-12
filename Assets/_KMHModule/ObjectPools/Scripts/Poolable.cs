using UnityEngine;

namespace _KMH_Framework.Pool
{
    public enum PoolType
    {
        None = -1,

        Projectile_9mmSMG_Default = 0,
        Projectile_9mmSMG_Tracer = 1,

        Projectile_30calMMG_Default = 2,
        Projectile_30calMMG_Tracer = 3,

        Projectile_50calHMG_Default = 4,
        Projectile_50calHMG_Tracer = 5,

        Shell_9mmSMG = 50,
        Shell_30calMMG = 51,
        Shell_50calHMG = 52,

        Impact_Default = 100,
        Impact_Flesh = 110,
    }

    [System.Serializable]
    public class Poolable // 데이터 시트로만 관리하기 위해, Queue를 생성/관리하지 않음.
    {
        [SerializeField]
        internal PoolType type;
        [SerializeField]
        internal GameObject prefab;

        [Space(10)]
        [SerializeField]
        internal int initCount = 10;
        [SerializeField]
        internal int maxCount = 100;
    }
}