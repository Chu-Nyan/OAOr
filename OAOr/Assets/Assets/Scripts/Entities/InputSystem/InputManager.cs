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
    private event Action LeftClickStarted;
    private event Action LeftClickCanceled;
    private event Action RightClickCanceled;
    private event Action RightClickStarted;

    public InputManager()
    {
        _action = new PlayerInputAction();
        _action.InGame.WASD.performed += OnWASDPerformed;
        _action.InGame.WASD.canceled += OnWASDCanceled;
        _action.InGame.MouseDelta.performed += OnMouseDelta;
        _action.InGame.LeftClick.started += OnLeftClickStarted;
        _action.InGame.LeftClick.canceled += OnLeftClickCanceled;
        _action.InGame.RightClick.canceled += OnRightClickCanceled;
        _action.InGame.RightClick.started += OnRightClickStarted;
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

    public void RegisterLeftClickStarted(Action action)
    {
        LeftClickStarted += action;
    }

    public void RegisterLeftClickCanceled(Action action)
    {
        LeftClickCanceled += action;
    }

    public void RegisterRightClickCanceled(Action action)
    {
        RightClickCanceled += action;
    }
    public void RegisterRightClickStarted(Action action)
    {
        RightClickStarted += action;
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

    private void OnLeftClickStarted(InputAction.CallbackContext value)
    {
        LeftClickStarted?.Invoke();
    }

    private void OnLeftClickCanceled(InputAction.CallbackContext value)
    {
        LeftClickCanceled?.Invoke();
    }

    private void OnRightClickStarted(InputAction.CallbackContext value)
    {
        RightClickStarted?.Invoke();
    }

    private void OnRightClickCanceled(InputAction.CallbackContext value)
    {
        RightClickCanceled?.Invoke();
    }
    #endregion
}