using System;
using UnityEngine;

public class PlayerBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _cameraArm;
    private UnitStatus _status;
    private PlayerAnimator _animator;
    private Transform _shootingPivot;
    private CameraController _cameraController;

    private float _cameraSensitivity = 0.2f;

    private Vector2 _moveDirection;
    private bool _isMoveing;

    private event Action FixedUpdated;

    public PlayerBehaviour(Rigidbody rigid, Transform camera, Animator animator, UnitStatus status, Transform pivot, CameraController cameraController)
    {
        _rigidbody = rigid;
        _cameraArm = camera;
        _status = status;
        _shootingPivot = pivot;

        _animator = new();
        _animator.Init(animator);
        _cameraController = cameraController;

        FixedUpdated += Move;
    }

    public void RegisterMovementActions()
    {
        InputManager.Instance.RegisterWASDPerformed(GetMoveDirection);
        InputManager.Instance.RegisterWASDCanceled(GetMoveDirection);
        InputManager.Instance.RegisterMouseDeltaPerformed(RotateCameraArm);
        InputManager.Instance.RegisterLeftClickStarted(UseSkill);
        InputManager.Instance.RegisterRightClickStarted(TurnOnShootingMode);
        InputManager.Instance.RegisterRightClickCanceled(TurnOffShootingMode);
    }

    private void UseSkill()
    {
        var skill = new Skill(DataManager.Instance.SkillDataContainer[SkillType.FireBall]);
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        var dir = ray.direction;
        if (Physics.Raycast(ray, out var hitinfo) == true)
            dir = (hitinfo.point - _shootingPivot.position).normalized;
        
        skill.Use(_shootingPivot.position, dir);
    }

    private void GetMoveDirection(Vector2 dir)
    {
        _isMoveing = dir != Vector2.zero;
        _animator.SetMoveingState(_isMoveing, dir);
        if (dir.y < 0)
            dir *= 0.3f;
        else if (Math.Abs(dir.x) == 1)
            _moveDirection *= 0.7f;

        _moveDirection = dir;
    }

    private void Move()
    {
        if (_isMoveing == false)
            return;

        var moveDir = (Vector3)_moveDirection;
        var forward = new Vector3(_cameraArm.forward.x, 0, _cameraArm.forward.z).normalized;
        var right = new Vector3(_cameraArm.right.x, 0, _cameraArm.right.z).normalized;
        moveDir = (forward * moveDir.y + right * moveDir.x) * _status[StatType.Speed].ModificationValue;

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

    public void TurnOnShootingMode()
    {
        _cameraController.ChangeCameraMode(false);
    }

    public void TurnOffShootingMode()
    {
        _cameraController.ChangeCameraMode(true);
    }

    public void OnInvokeFixedUpdated()
    {
        FixedUpdated?.Invoke();
    }
}
