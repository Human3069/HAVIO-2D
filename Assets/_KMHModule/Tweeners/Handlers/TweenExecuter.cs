using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VTSModule
{
    public class TweenExecuter : MonoBehaviour
    {
        private BaseTweener[] tweeners;
 
        private void Awake()
        {
            tweeners = GetComponentsInChildren<BaseTweener>(true);
        }

        private void OnEnable()
        {
            foreach (BaseTweener tweener in tweeners)
            {
                tweener.ForcellySet(false);
            }

            StartCoroutine(OnEnableRoutine());
        }

        private IEnumerator OnEnableRoutine()
        {
            List<UniTask.Awaiter> awaiterList = new List<UniTask.Awaiter>();
            bool isOn = true;

            while (true)
            {
                foreach (BaseTweener tweener in tweeners)
                {
                    UniTask.Awaiter awaiter = tweener.DoTweenAsync(isOn).GetAwaiter();
                    awaiterList.Add(awaiter);
                }

                foreach (UniTask.Awaiter awaiter in awaiterList)
                {
                    yield return new WaitWhile(() => awaiter.IsCompleted == false);
                }

                isOn = !isOn;
                awaiterList.Clear();
            }
        }
    }
}
