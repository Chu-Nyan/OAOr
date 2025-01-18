using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Transform _cameraArm;
    private PlayerInputHandler _inputHandler;

    private PlayerStatusData _status;

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
        var moveDir = _inputHandler.MoveDirection * _status.Speed;
        var nextPos = new Vector3(transform.position.x + moveDir.x, transform.position.y, transform.position.z + moveDir.y);
        nextPos = Vector3.MoveTowards(transform.position, nextPos, 3 * Time.fixedDeltaTime);
        _rigidbody.MovePosition(nextPos);
    }

    private void RotateCameraArm(Vector2 dir)
    {
        var v3otation = _cameraArm.rotation.eulerAngles;
        var x =dir.y + v3otation.x;
        x = x < 180f ? Mathf.Clamp(dir.y + v3otation.x, -1, 45) : Mathf.Clamp(dir.y + v3otation.x, 320, 361);
        _cameraArm.rotation = Quaternion.Euler(x, dir.x + v3otation.y, 0);
    }
}
