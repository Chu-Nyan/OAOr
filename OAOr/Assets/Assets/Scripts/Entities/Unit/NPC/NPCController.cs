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

    public void Init(int id)
    {
        _status = new(id);
    }
}
