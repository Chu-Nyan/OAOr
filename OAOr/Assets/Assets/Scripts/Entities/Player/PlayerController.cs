using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Transform _cameraArm;
    [SerializeField]
    private Animator _animator;
    private PlayerBehaviour _behaviourHandler;

    private PlayerStatusData _status;

    private void Awake()
    {
        _status = new();

        _behaviourHandler = new(_rigidbody, _cameraArm, _animator, _status);
        _behaviourHandler.RegisterMovementActions();
    }

    private void FixedUpdate()
    {
        _behaviourHandler.OnInvokeFixedUpdated();
    }
}
