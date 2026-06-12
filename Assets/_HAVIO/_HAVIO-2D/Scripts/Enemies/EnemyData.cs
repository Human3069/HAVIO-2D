using System;
using UnityEngine;

namespace HAVIO
{
    [Serializable]
    public class EnemyData
    {
#if UNITY_EDITOR
        [Header("=== Components ===")]
        public Transform EyeTransform;
        public SpriteRenderer[] TrackableJointRenderers;

        [Header("=== Values ===")]
        public float FindPlayerDistance = 10f;
        public float AttackPlayerDistance = 5f;

        [Space(10)]
        public bool IsShowLog = true;
#endif
    }
}