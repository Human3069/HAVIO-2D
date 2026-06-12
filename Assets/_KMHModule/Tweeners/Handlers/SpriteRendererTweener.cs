using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VTSModule
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRendererTweener : BaseTweener
    {
        private SpriteRenderer spriteRenderer;

        [Header("=== UI_ImageTweener ===")]
        [SerializeField]
        private Color fromColor = Color.white;
        [SerializeField]
        private Color toColor = Color.white;

        public async UniTask DoTweenAsync(Color endColor)
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = this.GetComponent<SpriteRenderer>();
            }
            Color startColor = spriteRenderer.color;
            this.CallTween(duration, curve, (normal) => OnTweened(startColor, endColor, normal));

            await this.WaitWhileTweening();
        }

        public override async UniTask DoTweenAsync(bool isOn, Action onTweenDone = null)
        {
            Color endColor = isOn ? toColor : fromColor;

            await DoTweenAsync(endColor);

            onTweenDone?.Invoke();
        }

        public override void ForcellySet(bool isOn)
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = this.GetComponent<SpriteRenderer>();
            }

            Color endColor = isOn ? toColor : fromColor;
            spriteRenderer.color = endColor;
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

        private void OnTweened(Color startColor, Color endColor, float normal)
        {
            spriteRenderer.color = Color.Lerp(startColor, endColor, normal);
        }
    }
}
