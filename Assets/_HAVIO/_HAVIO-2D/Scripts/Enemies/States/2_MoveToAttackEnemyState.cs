using UnityEngine;

namespace HAVIO
{
    public class MoveToAttackEnemyState : IEnemyState
    {
        private EnemyData _data;

        public void Enter(EnemyData data)
        {
            this._data = data;
        }
        
        public void Exit()
        {
            // 상태 종료 처리
        }

        public void Tick()
        {

        }

        public void SlowTick()
        {
            _data.DecideStateByDistance();
            _data.HandleFlip();

            Vector3 targetPosition = _data.Section.GetNearestFromPlayer();
            if (_data.IsInRange(targetPosition, 0.1f) == false)
            {
                _data.Enemy.Move(targetPosition);
            }
        }
    }
}