using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera _camera;


    public void SetTrackingTarget(Transform target)
    {
        _camera.Follow = target;
    }
}
