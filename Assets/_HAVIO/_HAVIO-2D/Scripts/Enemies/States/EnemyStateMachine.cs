using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace HAVIO
{
    public class EnemyStateMachine
    {
        private EnemyData _data;
        
        private IEnemyState currentState;
        private CancellationTokenSource source = null;
        
        public delegate void StateChangedDelegate(IEnemyState oldState, IEnemyState newState);
        public event StateChangedDelegate OnStateChangedEvent;
        
        public EnemyStateMachine(EnemyData data)
        {
            this._data = data;
        }

        public IEnemyState GetState()
        {
            return currentState;
        }

        public void ChangeState(IEnemyState newState)
        {
            IEnemyState oldState = currentState;

#if UNITY_EDITOR
            if (_data.IsShowLog == true)
            {
                string oldStateName = oldState == null ? "Null" : oldState.GetType().Name;
                string newStateName = newState == null ? "Null" : newState.GetType().Name;
                
                oldStateName = oldStateName.Replace("EnemyState", "");
                newStateName = newStateName.Replace("EnemyState", "");

                Debug.LogFormat("<color=yellow>" + oldStateName + " => " + newStateName + "</color>");
            }
#endif

            currentState?.Exit();
            currentState = newState;
            currentState?.Enter(_data);
            
            source?.Cancel();
            source = new CancellationTokenSource();
            if (newState != null)
            {
                TickProvider(source.Token).Forget();
            }

            if (newState != null)
            {
                SlowTickProvider(source.Token).Forget();
            }
            
            OnStateChangedEvent?.Invoke(oldState, newState);
        }

        private async UniTask TickProvider(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                currentState?.Tick();
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }

        private async UniTask SlowTickProvider(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                currentState?.SlowTick();
                await UniTask.WaitForSeconds(0.5f, cancellationToken: token);
            }
        }
        
        public void LogCurrentState()
        {
            if (currentState == null)
            {
                Debug.LogFormat("No current state.");
            }
            else
            {
                string currentStateName = currentState.GetType().Name;
                currentStateName = currentStateName.Replace("EnemyState", "");
                
                Debug.LogFormat("current : " + currentStateName);
            }
        }
    }
}