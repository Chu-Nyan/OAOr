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

    private UnitStatus _status;

    private void Init(UnitStatus status)
    {
        _status = status;
        _behaviourHandler = new(_rigidbody, _cameraArm, _animator, status);
        _behaviourHandler.RegisterMovementActions();
    }

    private void FixedUpdate()
    {
        _behaviourHandler.OnInvokeFixedUpdated();
    }
}
