using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO onPlayButtonPressedEvent;

    [SerializeField] private GameObject uiPanel;

    void Start()
    {
        onPlayButtonPressedEvent.onVoidEvent += HandlePlayButtonEvent;
    }

    private void OnDestroy()
    {
        if (onPlayButtonPressedEvent != null)
            onPlayButtonPressedEvent.onVoidEvent -= HandlePlayButtonEvent;
    }

    void HandlePlayButtonEvent()
    {
        uiPanel.SetActive(false);
    }
}