using UnityEngine;

namespace HAVIO
{
    public class Shell : MonoPoolable
    {
        [Header("=== Shell ===")]
        [SerializeField]
        private Vector2 additionalSpeedRange = new Vector2(0f, 0.5f);
        [SerializeField]
        private Vector2 additionalAngularVelocityRange = new Vector2(-10f, 10f);

        private new Rigidbody2D rigidbody;

        private void Awake()
        {
            rigidbody = this.GetComponent<Rigidbody2D>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (HelicopterController.HasInstance == true)
            {
                float additionalSpeed = Random.Range(additionalSpeedRange.x, additionalSpeedRange.y);
                Vector2 randomizedDirection = Random.insideUnitCircle.normalized;
                Vector2 additionalVelocity = randomizedDirection * additionalSpeed;
                rigidbody.linearVelocity = HelicopterController.Instance.LinearVelocity + additionalVelocity;

                float additionalAngularVelocity = Random.Range(additionalAngularVelocityRange.x, additionalAngularVelocityRange.y);
                rigidbody.angularVelocity = additionalAngularVelocity;
            }
        }

        public void OnBeforeEnablePool(Vector3 position, Quaternion rotation)
        {
            this.transform.position = position;
            this.transform.rotation = rotation;
        }
    }
}