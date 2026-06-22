using UnityEngine;
using UnityEngine.AI;

namespace HAVIO
{
    [System.Serializable]
    public class EnemyData
    {
        [Header("=== Serializable Values")]
        public float MoveSpeed = 1f;

        [Space(10)]
        public float SearchRange = 10f;
        public float AttackRange = 5f;

#if UNITY_EDITOR
        public bool IsShowLog = false;
#endif

        [Header("=== Serializable References")]
        public EnemyJoint Joint;
        public TilemapSection Section;

        [Space(10)]
        public Transform EyeTransform;

        // Non-Serializable References
        public Enemy Enemy { get; set; }
        public EnemyStateMachine StateMachine { get; set;}
        public EnemyAnimator Animator { get; set; }
        public Damageable Damageable { get; set; }
        
        public void Initialize(Enemy enemy)
        {
            Joint.Initialize(enemy);

            Enemy = enemy;
            StateMachine = new EnemyStateMachine(this);
            StateMachine.ChangeState(new PatrolEnemyState());
            Animator = new EnemyAnimator(enemy);
            Damageable = enemy.GetComponent<Damageable>();
        }
    }
}