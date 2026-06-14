using UnityEngine;

namespace HAVIO
{
    public class PatrolEnemyState : IEnemyState
    {
        private EnemyData _data;

        private Vector3 leftPoint;
        private Vector3 rightPoint;

        public void Enter(EnemyData data)
        {
            this._data = data;

            (leftPoint, rightPoint) = _data.Section.GetSectionPoints(0.5f);
            _data.Enemy.Move(leftPoint);
        }
        
        public void Exit()
        {
            _data.Enemy.StopMove();
        }

        public void Tick()
        {
           
        }

        public void SlowTick()
        {
            if (_data.IsInRange(leftPoint, 0.1f) == true)
            {
                _data.Enemy.Move(rightPoint);
            }
            else if (_data.IsInRange(rightPoint, 0.1f) == true)
            {
                _data.Enemy.Move(leftPoint);
            }

            _data.DecideStateByDistance();
            _data.HandleFlip();
        }
    }
}