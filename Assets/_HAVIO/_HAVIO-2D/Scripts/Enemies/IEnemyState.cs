namespace HAVIO
{
    public interface IEnemyState
    {
        void Enter(EnemyContext context, EnemyData data);
        void Exit();
        void Tick();
        void SlowTick(); // 0.5초마다 호출
    }
}