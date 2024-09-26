using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnvironmentButton : MonoBehaviour
{
    public bool isBeingHovered = false;
    [SerializeField] protected UI_InputReader _inputReader;

    [SerializeField] private UnityEvent onButtonPressed;

    protected virtual void Start()
    {
        _inputReader.onTouch += HandleTouch;
    }

    private void HandleTouch()
    {
        if (isBeingHovered)
        {
            onButtonPressed?.Invoke();
            ToggleHover(false);
        }
    }

    public virtual void ToggleHover(bool value)
    {
    }
}