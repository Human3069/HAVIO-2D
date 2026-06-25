using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HAVIO
{
    public class PatrolEnemyState : IEnemyState
    {
        private EnemyData _data;

        private TilemapSectionLine _line;
        private bool isInitialized = false;

        public void Enter(EnemyData data)
        {
            this._data = data;

            EnterAsync().Forget();
        }

        private async UniTaskVoid EnterAsync()
        {
            _line = await _data.Section.GetLineAsync();
            _data.Enemy.Move(_line.Left);

            isInitialized = true;
        }

        public void Exit()
        {
            isInitialized = false;
        }

        public void Tick()
        {
            
        }

        public void SlowTick()
        {
            if (isInitialized == false)
            {
                return;
            }

            _data.CheckPlayerDistance();

            if (_data.Enemy.IsArrived(_line.Left) == true)
            {
                bool isReverse = _data.Joint.IsFlipToMoving(_line.Right);
                _data.Joint.FlipToMove(isReverse);
                _data.Enemy.Move(_line.Right);
            }
            else if (_data.Enemy.IsArrived(_line.Right) == true)
            {
                bool isReverse = _data.Joint.IsFlipToMoving(_line.Left);
                _data.Joint.FlipToMove(isReverse);
                _data.Enemy.Move(_line.Left);
            }
        }
    }
}