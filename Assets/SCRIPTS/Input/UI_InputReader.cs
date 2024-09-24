using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_InputReader : MonoBehaviour
{
    private UI_InputMap _input;

    public event Action<Vector2> onTouchPosition;

    public event Action onTouch;

    // Start is called before the first frame update
    void Start()
    {
        _input = new UI_InputMap();
        _input.Enable();

        _input.UI.Touch.started += HandleTouchStarted;
        _input.UI.Touch.canceled += HandleTouchCanceled;
    }

    private void OnDestroy()
    {
        _input.UI.Touch.performed -= HandleTouchStarted;
        _input.UI.Touch.canceled -= HandleTouchCanceled;

        if (_input.UI.TouchPosition != null)
            _input.UI.TouchPosition.performed -= HandleTouchPosition;
        
        _input.Disable();
    }

    void HandleTouchPosition(InputAction.CallbackContext context)
    {
        if (context.performed)
            onTouchPosition?.Invoke(context.ReadValue<Vector2>());
        else if (context.canceled)
            onTouchPosition?.Invoke(Vector2.zero);
    }

    void HandleTouchStarted(InputAction.CallbackContext context)
    {
        _input.UI.TouchPosition.performed += HandleTouchPosition;
    }

    void HandleTouchCanceled(InputAction.CallbackContext context)
    {
        _input.UI.TouchPosition.performed -= HandleTouchPosition;
        onTouch?.Invoke();
    }
}