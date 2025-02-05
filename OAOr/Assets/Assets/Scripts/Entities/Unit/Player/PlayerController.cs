using UnityEngine;

public class PlayerController : MonoBehaviour, IStatProvider
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Transform _cameraArm;
    [SerializeField]
    private Transform _shootingPivot;
    [SerializeField]
    private Animator _animator;
    private PlayerBehaviour _behaviourHandler;

    private UnitStatus _status;

    public Transform CameraArm
    {
        get => _cameraArm;
    }

    public UnitStatus Status
    {
        get => _status;
    }

    public void Awake()
    {
        _status = new(Const.PlayerID);
    }

    public void Init(UnitStatusDTO dto, CameraController camera)
    {
        _status.InitData(dto);
        _behaviourHandler = new(_rigidbody, _cameraArm, _animator, _status, _shootingPivot, camera);
        _behaviourHandler.RegisterMovementActions();
    }

    private void FixedUpdate()
    {
        _behaviourHandler.OnInvokeFixedUpdated();
    }
}
