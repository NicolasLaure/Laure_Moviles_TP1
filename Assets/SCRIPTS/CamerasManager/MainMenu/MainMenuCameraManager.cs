using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MainMenuCameraManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO onPlayButtonPressedEvent;

    [SerializeField] private float startDelay = 0;
    [SerializeField] private CinemachineDollyCart dollyCart;

    [SerializeField] private CinemachinePathBase mainToSelectionPath;

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
        StartCoroutine(StartCameraCoroutine());
    }

    private IEnumerator StartCameraCoroutine()
    {
        yield return new WaitForSeconds(startDelay);

        dollyCart.m_Path = mainToSelectionPath;
        dollyCart.enabled = true;
    }
}