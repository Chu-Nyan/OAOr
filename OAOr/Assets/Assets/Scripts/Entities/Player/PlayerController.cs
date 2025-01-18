using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Transform _cameraArm;
    private PlayerInputHandler _inputHandler;

    private PlayerStatusData _status;


    private void Awake()
    {
        _status = new();

        _inputHandler = new(_rigidbody,_cameraArm,_status);
        _inputHandler.RegisterAllInputAction();
    }

    private void FixedUpdate()
    {
        _inputHandler.FixedUpdate();
    }

}
