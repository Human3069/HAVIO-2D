namespace HAVIO
{
    public class AttackEnemyState : IEnemyState
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
            _data.LookAtAll();
        }

        public void SlowTick()
        {
            _data.DecideStateByDistance();
            _data.HandleFlip();
        }
    }
}