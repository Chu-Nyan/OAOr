using UnityEngine;

public class PlayerInputHandler
{
    public Vector2 MoveDirection;

    public void RegisterAllInputAction()
    {
        InputManager.Instance.RegisterWASDPerformed(Move);
        InputManager.Instance.RegisterWASDCanceled(Move);
    }

    private void Move(Vector2 dir)
    {
        if (dir.y != 1)
            dir *= 0.3f;

        MoveDirection = dir;
    }
}
