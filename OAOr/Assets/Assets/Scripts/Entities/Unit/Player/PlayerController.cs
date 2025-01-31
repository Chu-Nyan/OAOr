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
        _status = new(UnitType.Player);
    }

    [SerializeField]
    private int asd = 0;
    [SerializeField]
    private Transform asdasdasd;
    public void Update()
    {
        var cameraAim = Camera.main.ScreenPointToRay(new Vector2(Screen.width *0.5f , Screen.height *0.5f));
        var realAim = new Ray(_shootingPivot.position, cameraAim.direction);
        var isSuccess = Physics.Raycast(realAim, out var hitInfo, 1000);
        var sss = Physics.Raycast(cameraAim, out var hitInfoo, 1000);
        var realrealRay = new Ray(_shootingPivot.position, (_shootingPivot.position - hitInfo.point).normalized);
        var realrealAim = Physics.Raycast(realrealRay, out var hitInfoooooo, 1000);

        Debug.Log($"슈팅 범위 : {isSuccess}, 카메라 시점 : {sss}");
        //asdasdasd.transform.position = hitInfoooooo.point;
    }

    public void Init(UnitStatusDTO dto, Transform camera)
    {
        _status.InitData(dto);
        _behaviourHandler = new(_rigidbody, _cameraArm, _animator, _status, _shootingPivot);
        _behaviourHandler.RegisterMovementActions();
    }

    private void FixedUpdate()
    {
        _behaviourHandler.OnInvokeFixedUpdated();
    }
}
