using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MainMenuCameraManager : MonoBehaviour
{
    [SerializeField] private float startDelay = 0;
    [SerializeField] private CinemachineDollyCart dollyCart;

    [SerializeField] private VoidEventChannelSO onPlayButtonPressedEvent;
    [SerializeField] private VoidEventChannelSO onSelectionReturnButtonPressedEvent;
    [SerializeField] private VoidEventChannelSO onCreditsButtonPressedEvent;
    [SerializeField] private VoidEventChannelSO onCreditsReturnButtonPressedEvent;
    [SerializeField] private VoidEventChannelSO onOptionsButtonPressedEvent;
    [SerializeField] private VoidEventChannelSO onOptionsReturnButtonPressedEvent;

    [SerializeField] private CinemachinePathBase mainToSelectionPath;
    [SerializeField] private CinemachinePathBase SelectionToMainPath;
    [SerializeField] private CinemachinePathBase mainToCreditsPath;
    [SerializeField] private CinemachinePathBase creditsToMainPath;
    [SerializeField] private CinemachinePathBase mainToOptionsPath;
    [SerializeField] private CinemachinePathBase optionsToMainPath;


    [SerializeField] private GameObject creditsGameObject;

    void Start()
    {
        onPlayButtonPressedEvent.onVoidEvent += HandlePlayButtonEvent;
        onCreditsButtonPressedEvent.onVoidEvent += HandleCreditsButtonEvent;
        onCreditsReturnButtonPressedEvent.onVoidEvent += HandleCreditsReturnButtonEvent;
        onSelectionReturnButtonPressedEvent.onVoidEvent += HandlePlayersSelectionReturnButtonEvent;
        onOptionsButtonPressedEvent.onVoidEvent += HandleOptionsButtonEvent;
        onOptionsReturnButtonPressedEvent.onVoidEvent += HandleOptionsReturnButtonEvent;
    }

    private void OnDestroy()
    {
        if (onPlayButtonPressedEvent != null)
            onPlayButtonPressedEvent.onVoidEvent -= HandlePlayButtonEvent;

        if (onCreditsButtonPressedEvent != null)
            onCreditsButtonPressedEvent.onVoidEvent -= HandleCreditsButtonEvent;

        if (onCreditsReturnButtonPressedEvent != null)
            onCreditsReturnButtonPressedEvent.onVoidEvent -= HandleCreditsReturnButtonEvent;

        if (onSelectionReturnButtonPressedEvent != null)
            onSelectionReturnButtonPressedEvent.onVoidEvent -= HandlePlayersSelectionReturnButtonEvent;

        if (onOptionsButtonPressedEvent != null)
            onOptionsButtonPressedEvent.onVoidEvent -= HandleOptionsButtonEvent;

        if (onOptionsReturnButtonPressedEvent != null)
            onOptionsReturnButtonPressedEvent.onVoidEvent -= HandleOptionsReturnButtonEvent;
    }

    void HandlePlayButtonEvent()
    {
        StartCoroutine(StartCameraCoroutine(mainToSelectionPath, startDelay));
    }

    void HandleCreditsButtonEvent()
    {
        StartCoroutine(StartCameraCoroutine(mainToCreditsPath, 0));
        creditsGameObject.SetActive(true);
    }

    void HandleCreditsReturnButtonEvent()
    {
        StartCoroutine(StartCameraCoroutine(creditsToMainPath, 0));
        creditsGameObject.SetActive(false);
    }

    void HandlePlayersSelectionReturnButtonEvent()
    {
        StartCoroutine(StartCameraCoroutine(SelectionToMainPath, 0));
    }

    void HandleOptionsButtonEvent()
    {
        StartCoroutine(StartCameraCoroutine(mainToOptionsPath, 0));
    }

    void HandleOptionsReturnButtonEvent()
    {
        StartCoroutine(StartCameraCoroutine(optionsToMainPath, 0));
    }

    private IEnumerator StartCameraCoroutine(CinemachinePathBase newPath, float duration)
    {
        yield return new WaitForSeconds(duration);

        dollyCart.m_Position = 0;
        dollyCart.m_Path = newPath;
        dollyCart.enabled = true;
    }
}