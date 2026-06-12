using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VTSModule
{
    public static class TweenerUtility 
    {
        private static Dictionary<BaseTweener, CancellationTokenSource> tweenDic = new Dictionary<BaseTweener, CancellationTokenSource>();

        public static void CallTween(this BaseTweener tweener, float duration, AnimationCurve curve, Action<float> onTween)
        {
            if (tweenDic.ContainsKey(tweener) == false)
            {
                tweenDic.Add(tweener, new CancellationTokenSource());
            }
            else if (tweenDic[tweener] == null ||
                     tweenDic[tweener].IsCancellationRequested == true)
            {
                tweenDic[tweener] = new CancellationTokenSource();
            }
            else
            {
                tweenDic[tweener].Cancel();
                tweenDic[tweener] = new CancellationTokenSource();
            }

            CancellationToken token = tweenDic[tweener].Token;
            TweenAsync(tweener, duration, curve, onTween, token).Forget();
        }

        private static async UniTask TweenAsync(BaseTweener tweener, float duration, AnimationCurve curve, Action<float> onTween, CancellationToken token)
        {
            AnimationCurve compressedCurve = curve.CompressedCurve(duration);

            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float evaluated = compressedCurve.Evaluate(elapsedTime);
                onTween?.Invoke(evaluated);

                if (Application.isPlaying == true)
                {
                    elapsedTime += Time.deltaTime;
                }
                else
                {
                    elapsedTime += (Time.deltaTime * 0.5f);
                }
     
                await UniTask.Yield(token);
            }

            float lastEvaluated = compressedCurve.Evaluate(duration);
            onTween?.Invoke(lastEvaluated);

            tweenDic.Remove(tweener);
        }

        public static async UniTask WaitWhileTweening(this BaseTweener tweener)
        {
            await UniTask.WaitWhile(tweener.IsTweening);
        }

        public static bool IsTweening(this BaseTweener tweener)
        {
            return tweenDic.ContainsKey(tweener) == true &&
                   tweenDic[tweener] != null &&
                   tweenDic[tweener].IsCancellationRequested == false;
        }

        public static void PauseTween(this BaseTweener tweener)
        {
            if (tweenDic.ContainsKey(tweener) == true &&
                tweenDic[tweener] != null)
            {
                tweenDic[tweener].Cancel();
            }
        }

        public static bool IsCancelRequested(this BaseTweener tweener)
        {
            if (tweenDic.ContainsKey(tweener) == false)
            {
                return false;
            }
            else if (tweenDic[tweener] == null)
            {
                return true;
            }

            return tweenDic[tweener].IsCancellationRequested;
        }

        public static AnimationCurve CompressedCurve(this AnimationCurve originCurve, float time = 1f)
        {
            if (originCurve == null ||
                originCurve.length == 0)
            {
                return new AnimationCurve();
            }

            float originalLastTime = originCurve[originCurve.length - 1].time;
            if (Mathf.Approximately(originalLastTime, 0f) == true)
            {
                return new AnimationCurve(originCurve.keys);
            }

            float scale = time / originalLastTime;
            Keyframe[] keyframes = new Keyframe[originCurve.length];

            for (int i = 0; i < originCurve.length; i++)
            {
                Keyframe oldKey = originCurve[i];
                float newTime = oldKey.time * scale;

#pragma warning disable CS0618
                keyframes[i] = new Keyframe(newTime, oldKey.value, oldKey.inTangent / scale, oldKey.outTangent / scale)
                {
                    tangentMode = oldKey.tangentMode,
                    weightedMode = oldKey.weightedMode,
                    inWeight = oldKey.inWeight,
                    outWeight = oldKey.outWeight
                };
#pragma warning restore CS0618
            }

            return new AnimationCurve(keyframes);
        }
    }
}
