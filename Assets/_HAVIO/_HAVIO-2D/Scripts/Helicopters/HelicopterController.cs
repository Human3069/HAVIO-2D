using _KMH_Framework;
using UnityEngine;

namespace HAVIO
{
    public class HelicopterController : MonoSingleton<HelicopterController>
    {
        private new Rigidbody2D rigidbody;

        [SerializeField]
        private Transform centerOfMassT;

        [Space(10)]
        [SerializeField]
        private float moveAmount;
        [SerializeField]
        private float rotateAmount;
        [SerializeField]
        private float maxTiltAngle = 40f;
     
        public Vector2 LinearVelocity
        {
            get
            {
                return rigidbody.linearVelocity;
            }
            set
            {
                rigidbody.linearVelocity = value;
            }
        }

        protected virtual void Awake()
        {
            rigidbody = this.GetComponent<Rigidbody2D>();
            rigidbody.centerOfMass = centerOfMassT.localPosition;
        }

        private void Update()
        {
            float moveDelta = PlayerInputHandler.Instance.Throttle * moveAmount * Time.deltaTime;
            rigidbody.AddForce(this.transform.up * moveDelta);

            float currentAngle = this.transform.rotation.eulerAngles.z;
            float targetAngle = PlayerInputHandler.Instance.Pitch * maxTiltAngle;
            float angleDelta = Mathf.DeltaAngle(currentAngle, targetAngle);
            float torque = angleDelta * rotateAmount * Time.deltaTime;
            rigidbody.AddTorque(torque);
        }
    }
}