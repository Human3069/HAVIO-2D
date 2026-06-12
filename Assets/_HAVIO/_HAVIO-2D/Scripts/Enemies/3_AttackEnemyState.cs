namespace HAVIO
{
    public class AttackEnemyState : IEnemyState
    {
        private EnemyContext _context;
        private EnemyData _data;
        
        public void Enter(EnemyContext context, EnemyData data)
        {
            this._context = context;
            this._data = data;
        }
        
        public void Exit()
        {
            // 상태 종료 처리
        }

        public void Tick()
        {
            _context.LookAtAll(_data);
        }

        public void SlowTick()
        {
            _context.DecideStateByDistance(_data);
            _context.HandleFlip(_data);
        }
    }
}