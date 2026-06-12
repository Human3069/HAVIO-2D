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
        
        public void SlowTick()
        {
            // 0.5초 간격 상태 유지 루프
        }
    }
}