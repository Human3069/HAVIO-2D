using System.Collections.Generic;
using UnityEngine;

namespace HAVIO
{
    public class TilemapSection : MonoBehaviour
    {
        [SerializeField]
        private List<BoxCollider2D> colliderList;

        private Vector3 leftPoint;
        private Vector3 rightPoint;

        private void Awake()
        {
            SortListByPositionX();
            EvaluatePoints();
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

        private void EvaluatePoints()
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
            this.leftPoint = new Vector2(firstBounds.min.x, firstBounds.max.y);
            this.rightPoint = new Vector2(lastBounds.max.x, lastBounds.max.y);
        }

        public Vector3 GetNearestFromPlayer()
        {
            Vector3 fromPosition = HelicopterController.Instance.transform.position;
            Vector3 lineDirection = rightPoint - leftPoint;
            Vector3 toPosition = fromPosition - leftPoint;

            float normal = Vector3.Dot(toPosition, lineDirection) / lineDirection.sqrMagnitude;
            normal = Mathf.Clamp01(normal);

            return leftPoint + normal * lineDirection;
        }

        public (Vector3, Vector3) GetSectionPoints(float offset)
        {
            Vector3 appliedLeftPoint = leftPoint + Vector3.right * offset;
            Vector3 appliedRightPoint = rightPoint + Vector3.left * offset;

            return (appliedLeftPoint, appliedRightPoint);
        }

        [ContextMenu("Get Tilemap Colliders")]
        private void GetTilemapColliders()
        {
            BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();
            colliderList = new List<BoxCollider2D>(colliders);
            
            SortListByPositionX();
        }
    }
}