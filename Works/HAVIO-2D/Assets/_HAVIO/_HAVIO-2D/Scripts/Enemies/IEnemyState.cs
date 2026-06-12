namespace HAVIO
{
    public interface IEnemyState
    {
        void Enter(EnemyContext context, EnemyData data);
        void Exit();
        void SlowTick(); // 0.5초마다 호출
    }
}