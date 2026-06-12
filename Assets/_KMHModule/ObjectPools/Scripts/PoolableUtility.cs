using _KMH_Framework.Pool;
using System;
using UnityEngine;

namespace _KMH_Framework
{
    public static class PoolableUtility
    {
        public static GameObject EnablePool(this PoolType type, Action<GameObject> onBeforeEnableAction = null)
        {
            GameObject enabledObj = PoolableManager.Instance.EnablePool(type, onBeforeEnableAction);
            return enabledObj;
        }

        public static T EnablePool<T>(this PoolType type, Action<T> onBeforeEnableAction = null) where T : Component
        {
            T enabledComponent = PoolableManager.Instance.EnablePool<T>(type, onBeforeEnableAction);
            return enabledComponent;
        }

        public static void DisablePool(this GameObject obj, PoolType type)
        {
            PoolableManager.Instance.DisablePool(type, obj);
        }

        public static PoolType GetPoolType(this ImpactType impactType, PoolType projectileType)
        {
            if (impactType == ImpactType.Default)
            {
                return PoolType.Impact_Default;
            }
            else if (impactType == ImpactType.Flesh)
            {
                return PoolType.Impact_Flesh;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(impactType), impactType, null);
            }
        }

        public static PoolType GetShellType(this PoolType poolType)
        {
            switch (poolType)
            {
                case PoolType.Projectile_9mmSMG_Default:
                case PoolType.Projectile_9mmSMG_Tracer:
                    return PoolType.Shell_9mmSMG;

                case PoolType.Projectile_30calMMG_Default:
                case PoolType.Projectile_30calMMG_Tracer:
                    return PoolType.Shell_30calMMG;

                case PoolType.Projectile_50calHMG_Default:
                case PoolType.Projectile_50calHMG_Tracer:
                    return PoolType.Shell_50calHMG;

                default:
                    throw new ArgumentOutOfRangeException(nameof(poolType), poolType, null);
            }
        }
    }
}