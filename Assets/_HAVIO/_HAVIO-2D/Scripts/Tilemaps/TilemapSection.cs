using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace HAVIO
{
    public class TilemapSection : MonoBehaviour
    {
        [SerializeField]
        private List<BoxCollider2D> colliderList;
        [SerializeField]
        private float offset = 0.5f;

        private bool isInitialized = false;
        private TilemapSectionLine line;

        private void Awake()
        {
            SortListByPositionX();
            EvaluateLine();
        }

        private void SortListByPositionX()
        {
            colliderList.Sort(SortComparison);
            int SortComparison(BoxCollider2D a, BoxCollider2D b)
            {
                float aPositionX = a.transform.position.x;
                float bPositionX = b.transform.position.x;

                return aPositionX.CompareTo(bPositionX);
            }
        }

        private void EvaluateLine()
        {
            if (colliderList == null ||
                colliderList.Count == 0)
            {
                Debug.LogWarning("Collider list is empty. Cannot evaluate points.");
                return;
            }

            BoxCollider2D firstCollider = colliderList[0];
            BoxCollider2D lastCollider = colliderList[colliderList.Count - 1];

            Bounds firstBounds = firstCollider.bounds;
            Bounds lastBounds = lastCollider.bounds;

            Vector3 leftPoint = new Vector2(firstBounds.min.x, firstBounds.max.y);
            Vector3 rightPoint = new Vector2(lastBounds.max.x, lastBounds.max.y);

            line = new TilemapSectionLine(leftPoint, rightPoint, offset);
            isInitialized = true;
        }

        public Vector3 GetClosestFromPlayer()
        {
            Vector3 fromPosition = HelicopterController.Instance.transform.position;
            Vector3 lineDirection = line.Direction;
            Vector3 toPosition = fromPosition - line.Left;

            float normal = Vector3.Dot(toPosition, lineDirection) / lineDirection.sqrMagnitude;
            normal = Mathf.Clamp01(normal);

            return line.Left + normal * lineDirection;
        }

        public async UniTask<TilemapSectionLine> GetLineAsync()
        {
            await UniTask.WaitUntil(() => isInitialized == true);
            return line;
        }

        [ContextMenu("Get Tilemap Colliders")]
        private void GetTilemapColliders()
        {
            BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();
            colliderList = new List<BoxCollider2D>(colliders);
            
            SortListByPositionX();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            line.DrawGizmos();
        }
#endif
    }
}