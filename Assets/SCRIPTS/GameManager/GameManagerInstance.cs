using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerInstance : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;

    [SerializeField] private TimerManager startingCountDown;
    [SerializeField] private TimerManager gameTimer;

    [SerializeField] private ControladorDeDescarga contr1;
    [SerializeField] private ControladorDeDescarga contr2;

    [SerializeField] private ContrCalibracion contrCalib1;
    [SerializeField] private ContrCalibracion contrCalib2;

    [SerializeField] private PalletMover tutorialP1;
    [SerializeField] private PalletMover tutorialP2;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate players
        gameManager.StartCountDown = startingCountDown;
        gameManager.GameTimer = gameTimer;
        gameManager.SpawnPlayers();

        gameManager.Player1.ContrCalib = contrCalib1;
        contr1.SetPlayer(gameManager.Player1);
        tutorialP1.InputReader = gameManager.Player1.GetComponent<InputReader>();

        if (gameManager.Player2 != null)
        {
            gameManager.Player2.ContrCalib = contrCalib2;
            contr2.SetPlayer(gameManager.Player2);
            tutorialP2.InputReader = gameManager.Player2.GetComponent<InputReader>();
        }

        //Set Cameras
        //Iniciar Tutorial

        StartCoroutine(InitCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        gameManager.UpdateGame();
    }

    IEnumerator InitCoroutine()
    {
        yield return null;
        gameManager.IniciarTutorial();
    }
}