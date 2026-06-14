using _KMH_Framework;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HAVIO
{
    public class InputActionHandler : MonoSingleton<InputActionHandler>
    {
        [Header("=== Movements ===")]
        [SerializeField]
        private InputActionReference pitchAction;
        [SerializeField]
        private InputActionReference throttleAction;

        [Header("=== Combats ===")]
        [SerializeField]
        private InputActionReference fireMainWeaponAction;
        [SerializeField]
        private InputActionReference fireSubWeaponAction;
        [SerializeField]
        private InputActionReference reloadAction;

        [NonSerialized]
        public float PitchInput;
        [NonSerialized]
        public float ThrottleInput;
        [NonSerialized]
        public bool IsFiringMain;
        public Action OnInputFireMain;

        [NonSerialized]
        public bool IsFiringSub;
        public Action OnInputFireSub;

        public Action OnInputReload;

        private void Awake()
        {
            // Movements
            pitchAction.action.Enable();
            throttleAction.action.Enable();

            // Combats
            fireMainWeaponAction.action.Enable();
            fireSubWeaponAction.action.Enable();
            reloadAction.action.Enable();
        }

        private void OnDestroy()
        {
            // Movements
            pitchAction.action.Disable();
            throttleAction.action.Disable();

            // Combats
            fireMainWeaponAction.action.Disable();
            fireSubWeaponAction.action.Disable();
            reloadAction.action.Disable();
        }

        private void Update()
        {
            PitchInput = pitchAction.action.ReadValue<float>();
            ThrottleInput = throttleAction.action.ReadValue<float>();

            IsFiringMain = fireMainWeaponAction.action.IsPressed();
            if (fireMainWeaponAction.action.WasPressedThisFrame() == true)
            {
                OnInputFireMain?.Invoke();
            }

            IsFiringSub = fireSubWeaponAction.action.IsPressed();
            if (fireSubWeaponAction.action.WasPressedThisFrame() == true)
            {
                OnInputFireSub?.Invoke();
            }

            if (reloadAction.action.WasPressedThisFrame() == true)
            {
                OnInputReload?.Invoke();
            }

#if false
            // Cameras
            if (moveAction.action.IsPressed() == true)
            {
                Vector2 moveValue = moveAction.action.ReadValue<Vector2>();
                OnValueChangedMoveAxis?.Invoke(moveValue);
            }

            if (rotateAction.action.IsPressed() == true)
            {
                float rotateValue = rotateAction.action.ReadValue<float>();
                OnValueChangedRotateAxis?.Invoke(rotateValue);
            }

            IsSprint = sprintAction.action.IsPressed();

            if (zoomAction.action.IsPressed() == true)
            {
                float zoomValue = zoomAction.action.ReadValue<float>();
                OnValueChangedZoomAxis?.Invoke(zoomValue);
            }

            // Formation Controllers
            IsSelectDown = selectAction.action.WasPressedThisFrame();
            IsSelecting = selectAction.action.IsPressed();
            IsSelectUp = selectAction.action.WasReleasedThisFrame();

            IsFormDown = formAction.action.WasPressedThisFrame();
            IsForming = formAction.action.IsPressed();
            IsFormUp = formAction.action.WasReleasedThisFrame();

            IsExecuting = executeAction.action.IsPressed();
            IsExecuteDown = executeAction.action.WasPressedThisFrame();

            if (cancelAction.action.WasPressedThisFrame() == true)
            {
                OnCancel?.Invoke();
            }

            IsShowSilhouette = showSilhouetteAction.action.IsPressed();

            // UIs
            IsSelectDiscrete = selectDiscreteAction.action.IsPressed();
            IsSelectContinuous = selectContinuousAction.action.IsPressed();
#endif
        }
    }
}