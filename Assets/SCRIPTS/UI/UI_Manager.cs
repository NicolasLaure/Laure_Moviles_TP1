using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManagerSo;

    [SerializeField] private PlayerUI singlePlayerUI;
    [SerializeField] private PlayerUI player1UI;
    [SerializeField] private PlayerUI player2UI;

    void Awake()
    {
        gameManagerSo.onToggleUI += HandleToggleUI;
    }

    private void OnDisable()
    {
        gameManagerSo.onToggleUI -= HandleToggleUI;
    }

    private void HandleToggleUI(bool value)
    {
        if (gameManagerSo.IsSinglePlayer())
        {
            singlePlayerUI.player = gameManagerSo.Player1;
            singlePlayerUI.ToggleOnOff(true);
            return;
        }

        player1UI.player = gameManagerSo.Player1;
        player1UI.ToggleOnOff(true);
        player2UI.player = gameManagerSo.Player2;
        player2UI.ToggleOnOff(true);
    }
}