using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace VTSModule
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UI_TMPTweener : BaseTweener
    {
        private TextMeshProUGUI tmp;

        [Header("=== UI_ImageTweener ===")]
        [SerializeField]
        private Color fromColor = Color.white;
        [SerializeField]
        private Color toColor = Color.white;

        public async UniTask DoTweenAsync(Color endColor)
        {
            if (tmp == null)
            {
                tmp = this.GetComponent<TextMeshProUGUI>();
            }
            Color startColor = tmp.color;
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
            if (tmp == null)
            {
                tmp = this.GetComponent<TextMeshProUGUI>();
            }

            Color endColor = isOn ? toColor : fromColor;
            tmp.color = endColor;
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
            tmp.color = Color.Lerp(startColor, endColor, normal);
        }
    }
}
