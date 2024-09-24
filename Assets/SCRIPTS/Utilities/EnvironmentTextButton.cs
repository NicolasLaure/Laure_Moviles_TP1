using TMPro;
using UnityEngine;

public class EnvironmentTextButton : EnvironmentButton
{
    [SerializeField] private TextMeshPro text;

    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightedColor;

    public override void ToggleHover(bool value)
    {
        isBeingHovered = value;

        if (value)
            text.color = highlightedColor;
        else
            text.color = normalColor;
    }
}