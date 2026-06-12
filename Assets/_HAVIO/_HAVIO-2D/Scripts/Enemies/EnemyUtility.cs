using UnityEngine;

namespace HAVIO
{
    public static class EnemyUtility
    {
        /// <summary>
        /// 적과 플레이어의 거리가 공격 가능 거리라면 공격 상태로 상태전이,
        /// 이동 가능 거리라면 이동 상태로 상태전이.
        /// 그 외의 거리라면 현상 유지.
        /// </summary>
        public static void DecideStateByDistance(this EnemyContext context, EnemyData data)
        {
            Vector3 targetPos = HelicopterController.Instance.transform.position;
            Vector3 myPos = data.EyeTransform.position;
            Vector3 targetDirection = (targetPos - myPos).normalized;
            RaycastHit2D hit = Physics2D.Raycast(myPos, targetDirection, Mathf.Infinity);

            IEnemyState currentState = context.StateMachine.GetState();

            if (hit.collider.TryGetComponent<HelicopterController>(out _) == true)
            {
                if (data.IsShowLog == true)
                {
                    Debug.DrawRay(myPos, targetDirection * hit.distance, Color.green, 0.5f);
                }

                if (hit.distance < data.AttackPlayerDistance)
                {
                    if (currentState is not AttackEnemyState)
                    {
                        context.StateMachine.ChangeState(new AttackEnemyState());
                    }
                }
                else if (hit.distance < data.FindPlayerDistance)
                {
                    if (currentState is not MoveToAttackEnemyState)
                    {
                        context.StateMachine.ChangeState(new MoveToAttackEnemyState());
                    }
                }
            }
            else
            {
                if (data.IsShowLog == true)
                {
                    Debug.DrawRay(myPos, targetDirection * hit.distance, Color.red, 0.5f);
                }

                if (currentState is not PatrolEnemyState)
                {
                    context.StateMachine.ChangeState(new PatrolEnemyState());
                }
            }
        }

        /// <summary>
        /// 뒤집혀진 상태 (EulerAngle 0, 180, 0) 가 되어야 하는지 여부
        /// </summary>
        /// <param name="context"></param>
        /// <returns>맞다면 True, 아니면 False</returns>
        public static bool IsFlippableReversed(this EnemyContext context, EnemyData data)
        {
            Vector3 targetPos = HelicopterController.Instance.transform.position;
            float xDiff = targetPos.x - data.EyeTransform.position.x;
            return xDiff >= 0;
        }

        /// <summary>
        /// 컴포넌트 베이스 및 피벗을 유지한 채 관절이 되는 스프라이트 렌더러를 뒤집습니다.
        /// </summary>
        /// <param name="isReversed">뒤집는다면 True, 원래 상태라면 False</param>
        /// <param name="trackableJointRenderers">관절로 사용되는 SpriteRenderer[]</param>
        public static void Flip(this EnemyContext context, bool isReversed, SpriteRenderer[] trackableJointRenderers)
        {
            if (isReversed == true)
            {
                context.Transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else
            {
                context.Transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }

            foreach (SpriteRenderer jointRenderer in trackableJointRenderers)
            {
                jointRenderer.flipY = isReversed;
            }
        }

        public static void HandleFlip(this EnemyContext context, EnemyData data)
        {
            bool isFlippable = IsFlippableReversed(context, data);
            Flip(context, isFlippable, data.TrackableJointRenderers);
        }

        /// <summary>
        /// 적이 플레이어를 바라보도록 관절 스프라이트 렌더러를 회전시킵니다.
        /// </summary>
        public static void LookAtAll(this EnemyContext context, EnemyData data)
        {
            Vector3 targetPos = HelicopterController.Instance.transform.position;
            Vector3 targetDirection = (targetPos - data.EyeTransform.position).normalized;

            foreach (SpriteRenderer jointRenderer in data.TrackableJointRenderers)
            {
                jointRenderer.transform.right = -targetDirection;
            }
        }
    }
}