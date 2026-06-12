using _KMH_Framework;
using UnityEngine;

namespace HAVIO
{
    public class PlayerInputHandler : MonoSingleton<PlayerInputHandler>
    {
        [ReadOnly]
        [SerializeField]
        private float _pitch;
        public float Pitch
        {
            get
            {
                return _pitch;
            }
            private set
            {
                _pitch = value;
            }
        }

        [ReadOnly]
        [SerializeField]
        private float _throttle;
        public float Throttle
        {
            get
            {
                return _throttle;
            }
            private set
            {
                _throttle = value;
            }
        }

        [Space(10)]
        [SerializeField]
        private Vector2 pitchRange = new Vector2(-1f, 1f);
        [SerializeField]
        private float pitchDeltaPerFrame = 5f;

        [Space(10)]
        [SerializeField]
        private Vector2 throttleRange = new Vector2(0f, 1f);
        [SerializeField]
        private float throttleDeltaPerFrame = 5f;

        protected virtual void Awake()
        {
            Pitch = (pitchRange.x + pitchRange.y) / 2f;
            Throttle = (throttleRange.x + throttleRange.y) / 2f;
        }

        private void Update()
        {
            GetInput();
        }

        private void GetInput()
        {
            // Throttle Inputs
            float throttleInput = InputActionHandler.Instance.ThrottleInput;
            bool isThrottleInput = throttleInput != 0f;
            float throttleDelta = throttleDeltaPerFrame * Time.deltaTime;

            if (isThrottleInput == true)
            {
                Throttle = Mathf.Clamp(Throttle + (throttleInput * throttleDelta), throttleRange.x, throttleRange.y);
            }
            else
            {
                Throttle = Mathf.MoveTowards(Throttle, (throttleRange.x + throttleRange.y) / 2f, throttleDelta);
            }

            // Pitch Inputs
            float pitchInput = InputActionHandler.Instance.PitchInput;
            bool isPitchInput = pitchInput != 0f;
            float pitchDelta = pitchDeltaPerFrame * Time.deltaTime;

            if (isPitchInput == true)
            {
                Pitch = Mathf.Clamp(Pitch + (pitchInput * pitchDelta), pitchRange.x, pitchRange.y);
            }
            else
            {
                Pitch = Mathf.MoveTowards(Pitch, (pitchRange.x + pitchRange.y) / 2f, pitchDelta);
            }
        }

        private void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 40;
            style.normal.textColor = Color.red;

            GUI.Label(new Rect(10, 10, 200, 20), "Pitch : " + Pitch.ToString("F2"), style);
            GUI.Label(new Rect(10, 50, 200, 20), "Throttle : " + Throttle.ToString("F2"), style);
        }
    }
}