using UnityEngine;
using UnityEngine.AI;

namespace HAVIO
{
    public class EnemyContext
    {
        public EnemyStateMachine StateMachine;
        
        public Transform Transform;
        public NavMeshAgent Agent;
        public EnemyAnimationController AnimationController;
        
        public EnemyContext(Enemy owner)
        {
            this.Transform = owner.transform;
            this.Agent = owner.GetComponent<NavMeshAgent>();
            
            Animator animator = owner.GetComponentInChildren<Animator>();
            this.AnimationController = new EnemyAnimationController(animator);
        }
        
        public void Initialize(EnemyStateMachine stateMachine)
        {
            this.StateMachine = stateMachine;
        }
    }
}