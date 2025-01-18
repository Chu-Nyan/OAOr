using UnityEditor;
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
        //Debug.DrawRay(_cameraArm.position, asd, Color.red);
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
        var angles = _cameraArm.rotation.eulerAngles;
        var x = angles.x - dir.y;
        x = x < 180f ? Mathf.Clamp(x, -1, 45) : Mathf.Clamp(x, 320, 361);

        transform.rotation = Quaternion.Euler(0, dir.x + angles.y, 0);
        _cameraArm.localRotation = Quaternion.Euler(x, 0, 0);
    }
}
