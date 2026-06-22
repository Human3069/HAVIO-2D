using UnityEngine;

namespace HAVIO
{
    public struct TilemapSectionLine 
    {
        public Vector3 Left;
        public Vector3 Right;

        public Vector3 Direction;

        public TilemapSectionLine(Vector3 left, Vector3 right, float offset)
        {
            this.Left = left + Vector3.right * offset;
            this.Right = right + Vector3.left * offset;

            this.Direction = this.Right - this.Left;
        }

        public void DrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Left, Right);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(Left, 0.1f);
            Gizmos.DrawSphere(Right, 0.1f);
        }
    }
}