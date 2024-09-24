using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRaycast : MonoBehaviour
{
    [SerializeField] private UI_InputReader input;
    [SerializeField] private LayerMask layerMask;

    private EnvironmentButton _currentTextButton;

    void Start()
    {
        input.onTouchPosition += HandleTouchPosition;
    }

    void HandleTouchPosition(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask))
        {
            Debug.Log(hit.transform.name);

            if (_currentTextButton != null && _currentTextButton != hit.transform.GetComponent<EnvironmentButton>())
                _currentTextButton.ToggleHover(false);

            _currentTextButton = hit.transform.GetComponent<EnvironmentButton>();
            _currentTextButton.ToggleHover(true);
        }
        else
        {
            if (_currentTextButton != null)
                _currentTextButton.ToggleHover(false);

            _currentTextButton = null;
        }
    }
}