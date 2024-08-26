using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerInstance : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate players
        gameManager.SpawnPlayers();

        //Set Cameras
        //Iniciar Tutorial

        StartCoroutine(InitCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator InitCoroutine()
    {
        yield return null;
        gameManager.IniciarTutorial();
    }
}