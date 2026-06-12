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
        }
    }
}