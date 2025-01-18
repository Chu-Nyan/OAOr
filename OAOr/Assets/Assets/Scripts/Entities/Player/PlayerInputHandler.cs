using System;
using UnityEngine;

public class PlayerInputHandler
{
    public Vector2 MoveDirection;

    public void RegisterAllInputAction(Action<Vector2> rotateAction)
    {
        InputManager.Instance.RegisterWASDPerformed(Move);
        InputManager.Instance.RegisterWASDCanceled(Move);
        InputManager.Instance.RegisterMouseDeltaPerformed(rotateAction);
    }

    private void Move(Vector2 dir)
    {
        if (dir.y != 1)
            dir *= 0.3f;

        MoveDirection = dir;
    }
}
