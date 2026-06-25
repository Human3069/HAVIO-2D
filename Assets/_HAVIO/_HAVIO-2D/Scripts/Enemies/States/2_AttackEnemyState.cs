namespace HAVIO
{
    public class AttackEnemyState : IEnemyState
    {
        private EnemyData _data;
        
        public void Enter(EnemyData data)
        {
            this._data = data;

            _data.Enemy.StopMove();
        }
        
        public void Exit()
        {
            // 상태 종료 처리
        }

        public void Tick()
        {
            bool? isReverse = _data.Joint.IsFlipToLookAtPlayer();
            if (isReverse != null)
            {
                _data.Joint.Flip(isReverse.Value);
                _data.Joint.LookAtPlayer();
            }

            if (_data.Enemy.IsMoving == true)
            {
                _data.Enemy.StopMove();
            }
        }

        public void SlowTick()
        {
            _data.CheckPlayerDistance();
        }
    }
}