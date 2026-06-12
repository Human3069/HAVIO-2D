using Unity.Cinemachine;
using UnityEngine;

public class SceneSetupHandler : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera cinemachineCamera;

    private void Awake()
    {
        cinemachineCamera.Lens.NearClipPlane = -1f;
        cinemachineCamera.Lens.FarClipPlane = 1f;
    }
}
