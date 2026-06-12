using UnityEngine;

namespace HAVIO
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private EnemyData data;
        
        private EnemyContext context;
        private EnemyStateMachine stateMachine;
        
        private void Awake()
        {
            context = new EnemyContext(this);
            stateMachine = new EnemyStateMachine(context, data);
            context.Initialize(stateMachine);
            context.StateMachine.ChangeState(new IdleEnemyState());
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            Gizmos.DrawWireSphere(data.EyeTransform.position, data.AttackPlayerDistance);

            Gizmos.color = new Color(1f, 1f, 0f, 0.5f);
            Gizmos.DrawWireSphere(data.EyeTransform.position, data.FindPlayerDistance);
        }
    }
}