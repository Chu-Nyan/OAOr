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

    public Transform CameraArm
    {
        get => _cameraArm;
    }

    public void Awake()
    {
        _status = new(UnitType.Player);
    }

    public void Init(UnitStatusDTO dto)
    {
        _status.InitData(dto);
        _behaviourHandler = new(_rigidbody, _cameraArm, _animator, _status);
        _behaviourHandler.RegisterMovementActions();
    }

    private void FixedUpdate()
    {
        _behaviourHandler.OnInvokeFixedUpdated();
    }
}
