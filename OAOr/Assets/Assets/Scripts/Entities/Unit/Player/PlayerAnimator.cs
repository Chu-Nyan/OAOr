using UnityEngine;

public class PlayerAnimator
{
    private Animator _animator;

    private readonly int _isMoveingID;

    public PlayerAnimator()
    {
        _isMoveingID = Animator.StringToHash("IsMoveing");
    }

    public void Init(Animator animator)
    {
        _animator = animator;
    }

    public void SetMoveingState(bool value, Vector2 dir)
    {
        if (_animator.GetBool(_isMoveingID) != value)
            _animator.SetBool(_isMoveingID, value);

        _animator.SetFloat("xDir", dir.x);
        _animator.SetFloat("yDir", dir.y);
    }
}
