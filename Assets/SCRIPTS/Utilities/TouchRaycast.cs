using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRaycast : MonoBehaviour
{
    [SerializeField] private UI_InputReader input;
    [SerializeField] private LayerMask layerMask;

    private EnvironmentButton _currentButton;

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

            if (_currentButton != null && _currentButton != hit.transform.GetComponent<EnvironmentButton>())
                _currentButton.ToggleHover(false);

            _currentButton = hit.transform.GetComponent<EnvironmentButton>();
            _currentButton.ToggleHover(true);
        }
        else
        {
            if (_currentButton != null)
                _currentButton.ToggleHover(false);

            _currentButton = null;
        }
    }
}