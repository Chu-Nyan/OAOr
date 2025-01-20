using UnityEngine;

public class NPCController : MonoBehaviour, IStatProvider
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Animator _animator;

    private UnitStatus _status;

    public UnitStatus Status
    {
        get => _status;
    }

    public NPCController()
    {
        _status = new(UnitType.Enemy);
    }
}
