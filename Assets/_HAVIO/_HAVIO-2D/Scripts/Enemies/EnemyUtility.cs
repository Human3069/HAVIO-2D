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
        public static void DecideStateByDistance(this EnemyData data)
        {
            Vector3 targetPos = HelicopterController.Instance.transform.position;
            Vector3 myPos = data.EyeTransform.position;
            Vector3 targetDirection = (targetPos - myPos).normalized;
            RaycastHit2D hit = Physics2D.Raycast(myPos, targetDirection, Mathf.Infinity);

            IEnemyState currentState = data.StateMachine.GetState();

            if (hit.collider.TryGetComponent<HelicopterController>(out _) == true)
            {
#if UNITY_EDITOR
                if (data.IsShowLog == true)
                {
                    Debug.DrawRay(myPos, targetDirection * hit.distance, Color.green, 0.5f);
                }
#endif

                if (hit.distance < data.AttackPlayerDistance)
                {
                    if (currentState is not AttackEnemyState)
                    {
                        data.StateMachine.ChangeState(new AttackEnemyState());
                    }
                }
                else if (hit.distance < data.FindPlayerDistance)
                {
                    if (currentState is not MoveToAttackEnemyState)
                    {
                        data.StateMachine.ChangeState(new MoveToAttackEnemyState());
                    }
                }
            }
            else
            {
#if UNITY_EDITOR
                if (data.IsShowLog == true)
                {
                    Debug.DrawRay(myPos, targetDirection * hit.distance, Color.red, 0.5f);
                }
#endif

                if (currentState is not PatrolEnemyState)
                {
                    data.StateMachine.ChangeState(new PatrolEnemyState());
                }
            }
        }

        /// <summary>
        /// 뒤집혀진 상태 (EulerAngle 0, 180, 0) 가 되어야 하는지 여부
        /// </summary>
        /// <param name="data"></param>
        /// <returns>맞다면 True, 아니면 False</returns>
        public static bool IsFlippableReversed(this EnemyData data)
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
        public static void Flip(this EnemyData data, bool isReversed)
        {
            if (isReversed == true)
            {
                data.Transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else
            {
                data.Transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }

            foreach (SpriteRenderer jointRenderer in data.TrackableJointRenderers)
            {
                jointRenderer.flipY = isReversed;
            }
        }

        public static void HandleFlip(this EnemyData data)
        {
            bool isFlippable = IsFlippableReversed(data);
            Flip(data, isFlippable);
        }

        /// <summary>
        /// 적이 플레이어를 바라보도록 관절 스프라이트 렌더러를 회전시킵니다.
        /// </summary>
        public static void LookAtAll(this EnemyData data)
        {
            Vector3 targetPos = HelicopterController.Instance.transform.position;
            Vector3 targetDirection = (targetPos - data.EyeTransform.position).normalized;

            foreach (SpriteRenderer jointRenderer in data.TrackableJointRenderers)
            {
                jointRenderer.transform.right = -targetDirection;
            }
        }

        public static bool IsInRange(this EnemyData data, Vector3 targetPosition, float offset)
        {
            Vector3 myPos = data.Transform.position;
            float distance = Vector3.Distance(myPos, targetPosition);

            return distance <= offset;
        }
    }
}