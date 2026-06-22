namespace HAVIO
{
    public interface IEnemyState
    {
        void Enter(EnemyData data);
        void Exit();
        void Tick(); // 매 프레임마다 호출
        void SlowTick(); // 0.5초마다 호출
    }
}