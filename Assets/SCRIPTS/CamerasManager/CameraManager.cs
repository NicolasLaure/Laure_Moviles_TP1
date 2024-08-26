using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private PlayerConfigSO singlePlayer;
    [SerializeField] private PlayerConfigSO playerOne;
    [SerializeField] private PlayerConfigSO playerTwo;

    [SerializeField] private Camera calibrationCamera1;
    [SerializeField] private Camera calibrationCamera2;
    [SerializeField] private Camera downloadCamera1;
    [SerializeField] private Camera downloadCamera2;

    private void Awake()
    {
        singlePlayer.calibrationCam = calibrationCamera1;
        singlePlayer.downloadCam = downloadCamera1;

        playerOne.calibrationCam = calibrationCamera1;
        playerOne.downloadCam = downloadCamera1;

        playerTwo.calibrationCam = calibrationCamera2;
        playerTwo.downloadCam = downloadCamera2;
    }
}