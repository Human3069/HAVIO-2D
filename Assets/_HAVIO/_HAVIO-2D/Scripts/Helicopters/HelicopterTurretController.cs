using _KMH_Framework;
using _KMH_Framework.Pool;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace HAVIO
{
    public class HelicopterTurretController : MonoBehaviour
    {
        [SerializeField]
        private Transform targetTransform;

        [Header("=== Turret Setting ===")]
        [SerializeField]
        private float minAngle = -60f;
        [SerializeField]
        private float maxAngle = 0f;

        [Space(10)]
        [SerializeField]
        private float rotateSpeed = 10f;

        private Animator animator;

        [Header("=== Weapon Setting ===")]
        [SerializeField]
        private List<PoolType> projectileRateList = new List<PoolType>();
        [SerializeField]
        private float fireRate = 0.1f;
        [SerializeField]
        private float spreadAngle = 1f;

        [Space(10)]
        [SerializeField]
        private int totalAmmo = 100;
        [ReadOnly]
        [SerializeField]
        private int _currentAmmo;
        private int CurrentAmmo
        {
            get
            {
                return _currentAmmo;
            }
            set
            {
                if (_currentAmmo != value)
                {
                    _currentAmmo = value;
                    if (value == 0)
                    {
                        ReloadAsync().Forget();
                    }
                }
            }
        }

        [SerializeField]
        private float reloadDuration = 5f;

        private int firedIndex = 0;
        private bool isFirable = true;
        private bool isReloading = true;
        private float remainedReloadDuration = 0f;

        private void Awake()
        {
            InputActionHandler.Instance.OnInputReload += OnInputReloadKey;

            animator = targetTransform.GetComponent<Animator>();

            CurrentAmmo = totalAmmo;
            isReloading = false;
            remainedReloadDuration = 0f;
        }

        private void OnDestroy()
        {
            if (InputActionHandler.HasInstance == true)
            {
                InputActionHandler.Instance.OnInputReload -= OnInputReloadKey;
            }
        }

        private void Update()
        {
            RotateTurretDirection();
            ClampTurretDirection();

            HandleFiring();
        }

        private void RotateTurretDirection()
        {
            Camera mainCamera = Camera.main;
            Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 desiredDirection = (worldMousePos - targetTransform.position).normalized;

            targetTransform.right = Vector3.RotateTowards(targetTransform.right, desiredDirection, rotateSpeed * Time.deltaTime, Mathf.Infinity);
        }

        private void ClampTurretDirection()
        {
            float currentAngle = targetTransform.localEulerAngles.z;
            if (currentAngle >= 180f && currentAngle < 360f)
            {
                if (currentAngle < (360f + minAngle))
                {
                    currentAngle = (360 + minAngle);
                }
            }
            else
            {
                if (currentAngle > maxAngle)
                {
                    currentAngle = maxAngle;
                }
            }

            targetTransform.localEulerAngles = new Vector3(0f, 0f, currentAngle);
        }

        private void HandleFiring()
        {
            if (InputActionHandler.Instance.IsFiringMain == true &&
                isFirable == true &&
                isReloading == false)
            {
                FireMainAsync().Forget();
            }
        }

        private void OnInputReloadKey()
        {
            if (isReloading == false &&
                CurrentAmmo != totalAmmo)
            {
                ReloadAsync().Forget();
            }
        }

        private async UniTaskVoid ReloadAsync()
        {
            isReloading = true;
            CurrentAmmo = 0;

            remainedReloadDuration = reloadDuration;
            while (remainedReloadDuration > 0f)
            {
                remainedReloadDuration -= Time.deltaTime;
                await UniTask.Yield();
            }
            remainedReloadDuration = 0f;

            CurrentAmmo = totalAmmo;
            isReloading = false;

            if (InputActionHandler.Instance.IsFiringMain == true &&
                isFirable == true)
            {
                FireMainAsync().Forget();
            }
        }

        private async UniTaskVoid FireMainAsync()
        {
            while (InputActionHandler.Instance.IsFiringMain == true &&
                   isReloading == false)
            {
                isFirable = false;
                FireOneShotAsync().Forget();

                await UniTask.WaitForSeconds(fireRate);
                isFirable = true;
            }
        }

        private async UniTaskVoid FireOneShotAsync()
        {
            int frameCount = 2;
            for (int i = 0; i < frameCount; i++)
            {
                int firingIndex = Random.Range(0, 3);
                animator.SetTrigger("Fire_" + firingIndex);

                if (i != frameCount - 1)
                {
                    await UniTask.Yield();
                }
            }

            float angle = targetTransform.eulerAngles.z + Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
            Quaternion spreadedRotation = Quaternion.Euler(0f, 0f, angle);

            PoolType type = projectileRateList[firedIndex];
            PoolType shellType = type.GetShellType();
            type.EnablePool<Projectile>(x => x.OnBeforeEnablePool(targetTransform.position, spreadedRotation));
            shellType.EnablePool<Shell>(x => x.OnBeforeEnablePool(targetTransform.position, targetTransform.rotation));

            firedIndex = (firedIndex + 1) % projectileRateList.Count;
            CurrentAmmo = Mathf.Clamp(CurrentAmmo - 1, 0, totalAmmo);
        }

        private void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 40;
            style.normal.textColor = Color.green;

            GUI.Label(new Rect(310, 10, 200, 20), "currentAmmo : " + CurrentAmmo, style);
            GUI.Label(new Rect(310, 50, 200, 20), "remainedReloadDuration : " + remainedReloadDuration.ToString("F2"), style);
        }
    }
}