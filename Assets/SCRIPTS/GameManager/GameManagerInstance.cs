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

    private bool _isPaused;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate players
        gameManager.StartCountDown = startingCountDown;
        gameManager.GameTimer = gameTimer;
        gameManager.ResetTimer();
        gameManager.EstAct = GameManagerSO.EstadoJuego.Calibrando;
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
        if (_isPaused)
            return;

        gameManager.UpdateGame();
    }

    IEnumerator InitCoroutine()
    {
        yield return null;
        gameManager.IniciarTutorial();
    }

    public void TogglePause(bool value)
    {
        _isPaused = value;
        if (_isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}