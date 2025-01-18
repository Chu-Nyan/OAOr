using Library.DesignPattern;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private readonly PlayerInputAction _action;

    private event Action<Vector2> WASDPerformed;
    private event Action<Vector2> WASDCanceled;
    private event Action<Vector2> MouseDeltaPerformed;

    public InputManager()
    {
        _action = new PlayerInputAction();
        _action.InGame.WASD.performed += OnWASDPerformed;
        _action.InGame.WASD.canceled += OnWASDCanceled;
        _action.InGame.MouseDelta.performed += OnMouseDelta;
    }

    public void SetActive(bool value)
    {
        if (value == true)
            _action.Enable();
        else
            _action.Disable();
    }

    public void RegisterWASDPerformed(Action<Vector2> action)
    {
        WASDPerformed += action;
    }

    public void RegisterWASDCanceled(Action<Vector2> action)
    {
        WASDCanceled += action;
    }

    public void RegisterMouseDeltaPerformed(Action<Vector2> action)
    {
        MouseDeltaPerformed += action;
    }

    public void UnregisterWASDPerformed(Action<Vector2> action)
    {
        WASDPerformed -= action;
    }

    public void UnregisterWASDCanceled(Action<Vector2> action)
    {
        WASDCanceled -= action;
    }

    public void UnregisterMouseDeltaPerformed(Action<Vector2> action)
    {
        MouseDeltaPerformed -= action;
    }

    #region 키 입력 이벤트
    private void OnWASDPerformed(InputAction.CallbackContext value)
    {
        WASDPerformed?.Invoke(value.ReadValue<Vector2>());
    }

    private void OnWASDCanceled(InputAction.CallbackContext value)
    {
        WASDCanceled?.Invoke(value.ReadValue<Vector2>());
    }

    private void OnMouseDelta(InputAction.CallbackContext value)
    {
        MouseDeltaPerformed?.Invoke(value.ReadValue<Vector2>());
    }
    #endregion
}