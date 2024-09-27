using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameManagerInstance gameManager;

    void OnEnable()
    {
        gameManager.TogglePause(true);
    }

    private void OnDisable()
    {
        gameManager.TogglePause(false);
    }

    public void PauseToggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void PauseToggle(bool value)
    {
        gameObject.SetActive(value);
    }

    public void MainMenu()
    {
        Loader.ChangeScene(1);
    }
}