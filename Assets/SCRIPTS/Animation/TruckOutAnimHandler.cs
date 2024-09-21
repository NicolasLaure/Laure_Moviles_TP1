using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckOutAnimHandler : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO onPlayButtonPressedEvent;

    [SerializeField] private Animator _animator;

    void Start()
    {
        onPlayButtonPressedEvent.onVoidEvent += HandlePlayButtonPressed;
    }

    private void OnDestroy()
    {
        if (onPlayButtonPressedEvent != null)
            onPlayButtonPressedEvent.onVoidEvent -= HandlePlayButtonPressed;
    }
    
    void HandlePlayButtonPressed()
    {
        _animator.enabled = true;
    }
}