using System;
using UnityEngine;

public class PlayerInputHandler
{
    private Rigidbody _rigidbody;
    private Transform _cameraArm;
    private PlayerStatusData _status;

    private float _cameraSensitivity = 0.2f;

    private Vector2 _moveDirection;
    private event Action FixedUpdated;

    public PlayerInputHandler(Rigidbody rigid, Transform camera, PlayerStatusData status)
    {
        _rigidbody = rigid;
        _cameraArm = camera;
        _status = status;
        FixedUpdated += Move;
    }

    public void RegisterAllInputAction()
    {
        InputManager.Instance.RegisterWASDPerformed(GetMoveDirection);
        InputManager.Instance.RegisterWASDCanceled(GetMoveDirection);
        InputManager.Instance.RegisterMouseDeltaPerformed(RotateCameraArm);
    }

    public void FixedUpdate()
    {
        FixedUpdated?.Invoke();
    }

    private void GetMoveDirection(Vector2 dir)
    {
        if (dir.y != 1)
            dir *= 0.3f;

        _moveDirection = dir;
    }

    private void Move()
    {
        var moveDir = (Vector3)_moveDirection;
        var forward = new Vector3(_cameraArm.forward.x, 0, _cameraArm.forward.z).normalized;
        var right = new Vector3(_cameraArm.right.x, 0, _cameraArm.right.z).normalized;
        moveDir = (forward * moveDir.y + right * moveDir.x) * _status.Speed;

        var nextPos = Vector3.MoveTowards(_rigidbody.position, _rigidbody.position + moveDir, 3 * Time.fixedDeltaTime);
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
