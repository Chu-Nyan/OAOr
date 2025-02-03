using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static readonly Vector3 NomalModeDamping = new(0.3f,0f, 0.3f);
    private static readonly Vector3 ShootingModeDamping = Vector3.zero;

    private static readonly float NomalModeDistance = 2.2f;
    private static readonly float ShootingModeDistance = 1.2f;

    [SerializeField]
    private CinemachineCamera _camera;
    [SerializeField]
    private CinemachineThirdPersonFollow _thirdPersonFollow;
    private Coroutine _cameraModeCoroutine;

    public void SetTrackingTarget(Transform target)
    {
        _camera.Follow = target;
    }

    public void ChangeCameraMode(bool isNomal)
    {
        float dis;
        Vector3 damping;
        if (isNomal == false)
        {
            dis = ShootingModeDistance;
            damping = ShootingModeDamping;
        }
        else
        {
            dis = NomalModeDistance;
            damping = NomalModeDamping;
        }

        if (_cameraModeCoroutine != null)
        {
            StopCoroutine(_cameraModeCoroutine);
        }
        _cameraModeCoroutine = StartCoroutine(UpdateCameraMode(damping, dis));
    }

    private IEnumerator UpdateCameraMode(Vector3 targetDamping, float targetDistance)
    {
        _thirdPersonFollow.Damping = targetDamping;
        while (Mathf.Abs(_thirdPersonFollow.CameraDistance - targetDistance) > 0.01f)
        {
            var nextDistance = Mathf.Lerp(_thirdPersonFollow.CameraDistance, targetDistance, 0.5f);
            _thirdPersonFollow.CameraDistance = nextDistance;
            yield return null;
        }
        _thirdPersonFollow.CameraDistance = targetDistance;
    }
}
