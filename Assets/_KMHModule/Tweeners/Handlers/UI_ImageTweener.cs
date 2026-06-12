using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace VTSModule
{
    [RequireComponent(typeof(Image))]
    public class UI_ImageTweener : BaseTweener
    {
        private Image image;

        [Header("=== UI_ImageTweener ===")]
        [SerializeField]
        private Color fromColor = Color.white;
        [SerializeField]
        private Color toColor = Color.white;

        public async UniTask DoTweenAsync(Color endColor)
        {
            if (image == null)
            {
                image = this.GetComponent<Image>();
            }
            Color startColor = image.color;
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
            if (image == null)
            {
                image = this.GetComponent<Image>();
            }

            Color endColor = isOn ? toColor : fromColor;
            image.color = endColor;
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
            image.color = Color.Lerp(startColor, endColor, normal);
        }
    }
}
