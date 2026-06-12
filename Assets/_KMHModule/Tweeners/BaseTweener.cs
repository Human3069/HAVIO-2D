
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VTSModule
{
    public abstract class BaseTweener : MonoBehaviour
    {
        [Header("=== BaseTweener ===")]
        [SerializeField]
        protected float duration = 0.3f;
        [SerializeField]
        protected AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        public abstract UniTask DoTweenAsync(bool isOn, Action onTweenDone = null);

        public abstract void ForcellySet(bool isOn);

        public abstract void ForcellySet(bool isOn, bool isSetActive);
    }
}
