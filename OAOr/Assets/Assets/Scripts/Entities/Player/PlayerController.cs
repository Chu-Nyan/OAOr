using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Transform _cameraArm;
    private PlayerInputHandler _inputHandler;

    private PlayerStatusData _status;

    private float _cameraSensitivity = 0.2f;

    private void Start()
    {
        _inputHandler = new();
        _status = new();
        _inputHandler.RegisterAllInputAction(RotateCameraArm);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var moveDir = (Vector3)_inputHandler.MoveDirection;
        var forward = new Vector3(_cameraArm.forward.x, 0, _cameraArm.forward.z).normalized;
        var right = new Vector3(_cameraArm.right.x, 0, _cameraArm.right.z).normalized;
        moveDir = forward * moveDir.y + right * moveDir.x;
        var nextPos = Vector3.MoveTowards(transform.position, transform.position + moveDir, 3 * Time.fixedDeltaTime);
        _rigidbody.MovePosition(nextPos);
    }

    private void RotateCameraArm(Vector2 dir)
    {
        dir *= _cameraSensitivity;
        var angles = _cameraArm.rotation.eulerAngles;
        var x = angles.x - dir.y;
        x = x < 180f ? Mathf.Clamp(x, -1, 45) : Mathf.Clamp(x, 320, 361);

        _rigidbody.rotation = Quaternion.Euler(0, dir.x + angles.y, 0);
        _cameraArm.localRotation = Quaternion.Euler(x, 0, 0);
    }
}
