using UnityEngine;

namespace HAVIO
{
    public static class EnemyUtility 
    {
        public static void CheckPlayerDistance(this EnemyData data)
        {
            Vector3 enemyPosition = data.EyeTransform.position;
            Vector3 playerPosition = HelicopterController.Instance.transform.position;

            float distance = Vector3.Distance(enemyPosition, playerPosition);
            bool isInAttackRange = distance <= data.AttackRange;
            bool isInSearchRange = distance <= data.SearchRange;

            if (isInAttackRange == true)
            {
                RaycastHit2D hit = Physics2D.Raycast(enemyPosition, playerPosition - enemyPosition, data.AttackRange);
                if (hit.collider.gameObject == HelicopterController.Instance.gameObject)
                {
                    data.StateMachine.ChangeState(new AttackEnemyState());
                }
                else
                {
                    data.StateMachine.ChangeState(new PatrolEnemyState());
                }
            }
            else if (isInSearchRange == true)
            {
                RaycastHit2D hit = Physics2D.Raycast(enemyPosition, playerPosition - enemyPosition, data.SearchRange);
                if (hit.collider.gameObject == HelicopterController.Instance.gameObject)
                {
                    data.StateMachine.ChangeState(new MoveToAttackEnemyState());
                }
                else 
                {
                    data.StateMachine.ChangeState(new PatrolEnemyState());
                }
            }
            else
            {
                data.StateMachine.ChangeState(new PatrolEnemyState());
            }
        }
    }
}