using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnvironmentButton : MonoBehaviour
{
    public bool isBeingHovered = false;

    [SerializeField] private UI_InputReader _inputReader;

    [SerializeField] private TextMeshPro text;

    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightedColor;

    [SerializeField] private UnityEvent onButtonPressed;
    void Start()
    {
        _inputReader.onTouch += HandleTouch;
    }

    private void HandleTouch()
    {
        if (isBeingHovered)
            onButtonPressed?.Invoke();
    }

    public void ToggleHover(bool value)
    {
        isBeingHovered = value;

        if (value)
            text.color = highlightedColor;
        else
            text.color = normalColor;
    }
}