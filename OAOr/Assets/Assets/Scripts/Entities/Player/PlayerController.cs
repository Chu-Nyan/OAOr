using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    private PlayerInputHandler _inputHandler;

    private PlayerStatusData _status;

    private void Start()
    {
        _inputHandler = new();
        _status = new();
        _inputHandler.RegisterAllInputAction();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var moveDir = _inputHandler.MoveDirection * _status.Speed;
        Debug.Log(moveDir);
        var nextPos = new Vector3(transform.position.x + moveDir.x, transform.position.y, transform.position.z + moveDir.y);
        nextPos = Vector3.MoveTowards(transform.position, nextPos, 3*Time.fixedDeltaTime);
        _rigidbody.MovePosition(nextPos);
    }
}
