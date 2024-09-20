using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerPosition : MonoBehaviour
{
    [SerializeField] private Vector3EventChannelSO onPlayer1LastPositionUpdate;
    [SerializeField] private Vector3EventChannelSO onPlayer2LastPositionUpdate;
    [SerializeField] private Vector3EventChannelSO onLastPlayerPositionUpdate;

    private float defaultZValue = -100;
    private Vector3 player1LastPosition;
    private Vector3 player2LastPosition;
    private Vector3 lastPosition;

    private void Start()
    {
        player1LastPosition = new Vector3(0, 0, defaultZValue);
        player2LastPosition = new Vector3(0, 0, defaultZValue);
        lastPosition = new Vector3(0, 0, defaultZValue);

        onPlayer1LastPositionUpdate.onVector3Event += HandlePlayerOneNewPosition;
        onPlayer2LastPositionUpdate.onVector3Event += HandlePlayerTwoNewPosition;
    }

    private void OnDestroy()
    {
        if (onPlayer1LastPositionUpdate)
            onPlayer1LastPositionUpdate.onVector3Event -= HandlePlayerOneNewPosition;
        if (onPlayer2LastPositionUpdate)
            onPlayer2LastPositionUpdate.onVector3Event -= HandlePlayerTwoNewPosition;
    }

    private void HandlePlayerOneNewPosition(Vector3 newPosition)
    {
        if (newPosition.z > player1LastPosition.z)
        {
            player1LastPosition = newPosition;
            CheckLastPoint();
        }
    }

    private void HandlePlayerTwoNewPosition(Vector3 newPosition)
    {
        if (newPosition.z > player2LastPosition.z)
        {
            player2LastPosition = newPosition;
            CheckLastPoint();
        }
    }

    private void CheckLastPoint()
    {
        Vector3 lastPlayerPosition = lastPosition;
        
        if (player1LastPosition.z < player2LastPosition.z)
            lastPlayerPosition = player1LastPosition;
        else
            lastPlayerPosition = player2LastPosition;
        
        if (player2LastPosition.z == defaultZValue)
            lastPlayerPosition = player1LastPosition;

        if (lastPlayerPosition.z > lastPosition.z)
        {
            lastPosition = lastPlayerPosition;
            onLastPlayerPositionUpdate.RaiseEvent(lastPosition);
        }
    }
}