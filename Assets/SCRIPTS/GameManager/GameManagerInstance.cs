using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerInstance : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;

    [SerializeField] private TimerManager startingCountDown;
    [SerializeField] private TimerManager gameTimer;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate players
        gameManager.StartCountDown = startingCountDown;
        gameManager.GameTimer = gameTimer;
        gameManager.SpawnPlayers();

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