using UnityEngine;

namespace HAVIO
{
    public class MoveToAttackEnemyState : IEnemyState
    {
        private EnemyData _data;
        private bool isStateMaintaining;

        public void Enter(EnemyData data)
        {
            this._data = data;
            isStateMaintaining = true;
        }
        
        public void Exit()
        {
            isStateMaintaining = false;
        }

        public void Tick()
        {
            bool? isReverse = _data.Joint.IsFlipToLookAtPlayer();
            if (isReverse != null)
            {
                _data.Joint.Flip(isReverse.Value);
                _data.Joint.LookAtPlayer();
            }
        }

        public void SlowTick()
        {
            if (isStateMaintaining == false)
            {
                return;
            }

            _data.CheckPlayerDistance();

            Vector3 closestPoint = _data.Section.GetClosestFromPlayer();
            if (_data.Enemy.IsArrived(closestPoint) == false)
            {
                bool isReverse = _data.Joint.IsFlipToMoving(closestPoint);
                _data.Joint.Flip(isReverse);
                _data.Enemy.Move(closestPoint);
            }
        }
    }
}