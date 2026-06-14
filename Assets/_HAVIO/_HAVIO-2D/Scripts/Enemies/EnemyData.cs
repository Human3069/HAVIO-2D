using System;
using UnityEngine;

namespace HAVIO
{
    [Serializable]
    public class EnemyData
    {
        [Header("=== Components ===")]
        public TilemapSection Section;

        [Space(10)]
        public Transform EyeTransform;
        public SpriteRenderer[] TrackableJointRenderers;
      
        [Header("=== Values ===")]
        public float MoveSpeed = 2f;

        // Battles
        public float FindPlayerDistance = 10f;
        public float AttackPlayerDistance = 5f;

#if UNITY_EDITOR
        [Space(10)]
        public bool IsShowLog = true;
#endif

        // NonSerialized
        public Enemy Enemy { get; set; }
        public Transform Transform { get; set; }
        public EnemyAnimationController Animator;
        public EnemyStateMachine StateMachine;
      
        public void Initialize(Enemy enemy)
        {
            this.Enemy = enemy;
            this.Transform = enemy.transform;
            this.Animator = new EnemyAnimationController(enemy);
            this.StateMachine = new EnemyStateMachine(this);
            this.StateMachine.ChangeState(new PatrolEnemyState());
        }
    }
}