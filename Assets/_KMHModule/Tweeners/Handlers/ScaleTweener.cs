using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VTSModule
{
    public class ScaleTweener : BaseTweener
    {
        [Header("=== ScaleTweener ===")]
        [SerializeField]
        private Vector3 fromScale = Vector3.one;
        [SerializeField]
        private Vector3 toScale = Vector3.one;

        public async UniTask DoTweenAsync(Vector3 endScale)
        {
            Vector3 startScale = this.transform.localScale;
            this.CallTween(duration, curve, (normal) => OnTweened(startScale, endScale, normal));

            await this.WaitWhileTweening();
        }

        public override async UniTask DoTweenAsync(bool isOn, Action onTweenDone = null)
        {
            Vector3 endScale = isOn ? toScale : fromScale;

            await DoTweenAsync(endScale);

            onTweenDone?.Invoke();
        }

        public override void ForcellySet(bool isOn)
        {
            Vector3 endScale = isOn ? toScale : fromScale;
            this.transform.localScale = endScale;
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

        private void OnTweened(Vector3 startScale, Vector3 endScale, float normal)
        {
            this.transform.localScale = Vector3.Lerp(startScale, endScale, normal);
        }
    }
}
