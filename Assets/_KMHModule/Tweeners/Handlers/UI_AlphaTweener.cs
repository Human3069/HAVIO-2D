using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VTSModule
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_AlphaTweener : BaseTweener
    {
        private CanvasGroup group;

        [Header("=== UI_AlphaTweener ===")]
        [SerializeField]
        private float fromAlpha = 0f;
        [SerializeField]
        private float toAlpha = 1f;

        public async UniTask DoTweenAsync(float endAlpha)
        {
            if (group == null)
            {
                group = this.GetComponent<CanvasGroup>();
            }
            float startAlpha = group.alpha;
            this.CallTween(duration, curve, (normal) => OnTweened(startAlpha, endAlpha, normal));

            await this.WaitWhileTweening();
        }

        public override async UniTask DoTweenAsync(bool isOn, Action onTweenDone = null)
        {
            float endColor = isOn ? toAlpha : fromAlpha;

            await DoTweenAsync(endColor);

            onTweenDone?.Invoke();
        }

        public virtual async UniTask DoTweenAsync(bool isOn, bool isSetActive, Action onTweenDone = null)
        {
            float endColor = isOn ? toAlpha : fromAlpha;
            if (isSetActive == true)
            {
                if (isOn == true)
                {
                    this.gameObject.SetActive(true);
                }
            }

            await DoTweenAsync(endColor);

            onTweenDone?.Invoke();
            if (isSetActive == true)
            {
                if (isOn == false)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }

        public override void ForcellySet(bool isOn)
        {
            if (group == null)
            {
                group = this.GetComponent<CanvasGroup>();
            }

            float endAlpha = isOn ? toAlpha : fromAlpha;
            group.alpha = endAlpha;
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

        private void OnTweened(float startAlpha, float endAlpha, float normal)
        {
            group.alpha = Mathf.Lerp(startAlpha, endAlpha, normal);
        }
    }
}
