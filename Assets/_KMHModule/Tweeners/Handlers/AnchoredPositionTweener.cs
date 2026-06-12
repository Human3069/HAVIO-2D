using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VTSModule
{
    [RequireComponent(typeof(RectTransform))]
    public class AnchoredPositionTweener : BaseTweener
    {
        private RectTransform rect;

        [Header("=== AnchoredPositionTweener ===")]
        [SerializeField]
        private Vector2 fromPosition;
        [SerializeField]
        private Vector2 toPosition;

        public async UniTask DoTweenAsync(Vector2 endPosition)
        {
            if (rect == null)
            {
                rect = this.transform as RectTransform;
            }

            Vector3 startPosition = rect.anchoredPosition;
            this.CallTween(duration, curve, (normal) => OnTweened(startPosition, endPosition, normal));

            await this.WaitWhileTweening();
        }

        public override async UniTask DoTweenAsync(bool isOn, Action onTweenDone = null)
        {
            Vector3 endPosition = isOn ? toPosition : fromPosition;

            await DoTweenAsync(endPosition);

            onTweenDone?.Invoke();
        }

        public override void ForcellySet(bool isOn)
        {
            if (rect == null)
            {
                rect = this.transform as RectTransform;
            }

            Vector3 endPosition = isOn ? toPosition : fromPosition;
            rect.anchoredPosition = endPosition;
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

        private void OnTweened(Vector3 startPosition, Vector3 endPosition, float normal)
        {
            rect.anchoredPosition = Vector3.Lerp(startPosition, endPosition, normal);
        }
    }
}
