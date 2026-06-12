using UnityEngine;

namespace HAVIO
{
    public class EnemyHuman : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer bodyRenderer;
        [SerializeField]
        private SpriteRenderer[] trackableJointRenderers;

        private void Update()
        {
            Vector3 targetPos = HelicopterController.Instance.transform.position;
            Vector3 targetDirection = (targetPos - this.transform.position).normalized;
            float xDiff = targetPos.x - this.transform.position.x;

            foreach (SpriteRenderer jointRenderer in trackableJointRenderers)
            {
                jointRenderer.transform.right = -targetDirection;
            }

            if (xDiff >= 0)
            {
                this.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                foreach (SpriteRenderer jointRenderer in trackableJointRenderers)
                {
                    jointRenderer.flipY = true;
                }
            }
            else
            {
                this.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                foreach (SpriteRenderer jointRenderer in trackableJointRenderers)
                {
                    jointRenderer.flipY = false;
                }
            }
        }
    }
}