using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VTSModule
{
    [RequireComponent(typeof(RectTransform))]
    public class RectSizeTweener : BaseTweener
    {
        private RectTransform rect;

        [Header("=== AnchoredPositionTweener ===")]
        [SerializeField]
        private Vector2 fromSize;
        [SerializeField]
        private Vector2 toSize;

        public int TargetingHeight
        {
            get
            {
                if (rect == null)
                {
                    rect = this.transform as RectTransform;
                }

                if (this.IsTweening() == true)
                {
                    return (int)targetingSize.y;
                }
                else
                {
                    return (int)rect.sizeDelta.y;
                }
            }
        }

        private Vector2 targetingSize;

        public async UniTask DoTweenAsync(Vector2 endSize)
        {
            targetingSize = endSize;
            if (rect == null)
            {
                rect = this.transform as RectTransform;
            }

            Vector3 startSize = rect.sizeDelta;
            this.CallTween(duration, curve, (normal) => OnTweened(startSize, endSize, normal));

            await this.WaitWhileTweening();
        }

        public override async UniTask DoTweenAsync(bool isOn, Action onTweenDone = null)
        {
            Vector3 endPosition = isOn ? toSize : fromSize;

            await DoTweenAsync(endPosition);

            onTweenDone?.Invoke();
        }

        public override void ForcellySet(bool isOn)
        {
            if (rect == null)
            {
                rect = this.transform as RectTransform;
            }

            Vector3 endSize = isOn ? toSize : fromSize;
            rect.sizeDelta = endSize;
        }

        public override void ForcellySet(bool isOn, bool isSetActive)
        {
            if (isSetActive == true && isOn == true)
            {
                this.gameObject.SetActive(true);
            }

            ForcellySet(isOn);

            if (isSetActive == true && isOn == false)
            {
                this.gameObject.SetActive(false);
            }
        }

        private void OnTweened(Vector3 startSize, Vector3 endSize, float normal)
        {
            rect.sizeDelta = Vector3.Lerp(startSize, endSize, normal);
        }
    }
}
