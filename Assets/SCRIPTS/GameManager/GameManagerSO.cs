using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameManagerSO", menuName = "GameManagement/GameManager", order = 0)]
public class GameManagerSO : ScriptableObject
{
    [SerializeField] private GameConfig config;

    public float TiempoDeJuego = 60;
    private float _currentGameTime = 60;

    public enum EstadoJuego
    {
        Calibrando,
        Jugando,
        Finalizado
    }

    public EstadoJuego EstAct = EstadoJuego.Calibrando;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerConfigSO singlePlayerConfig;
    [SerializeField] private PlayerConfigSO playerOneConfig;
    [SerializeField] private PlayerConfigSO playerTwoConfig;
    public Player Player1;
    public Player Player2;

    bool ConteoRedresivo = true;
    public float ConteoParaInicion = 3;
    public Rect ConteoPosEsc;
    public TimerManager StartCountDown;
    public TimerManager GameTimer;


    public float TiempEspMuestraPts = 3;

    //posiciones de los camiones para el tutorial
    public Vector3 PosCamion1Tuto = Vector3.zero;
    public Vector3 PosCamion2Tuto = Vector3.zero;

    //listas de GO que activa y desactiva por sub-escena
    //escena de tutorial
    public GameObject[] ObjsCalibracion1;

    public GameObject[] ObjsCalibracion2;

    //la pista de carreras
    public GameObject[] ObjsCarrera;

    public event Action<bool> onToggleUI;

    //--------------------------------------------------------//
    public void UpdateGame()
    {
        //REINICIAR
        if (Input.GetKey(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (EstAct)
        {
            case EstadoJuego.Calibrando:

                if (Input.GetKeyDown(KeyCode.W))
                {
                    Player1.Seleccionado = true;
                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Player2.Seleccionado = true;
                }

                break;


            case EstadoJuego.Jugando:

                //Turn on UI
                GameTimer.ToggleOnOff(true);
                //SKIP LA CARRERA
                if (Input.GetKey(KeyCode.Alpha9))
                {
                    _currentGameTime = 0;
                }

                if (_currentGameTime <= 0)
                {
                    FinalizarCarrera();
                }

                if (ConteoRedresivo)
                {
                    StartCountDown.ToggleOnOff(true);

                    ConteoParaInicion -= T.GetDT();
                    if (ConteoParaInicion < 0)
                    {
                        EmpezarCarrera();
                        ConteoRedresivo = false;
                        StartCountDown.ToggleOnOff(false);
                    }

                    if (ConteoParaInicion > 1)
                    {
                        StartCountDown.UpdateText(ConteoParaInicion.ToString("0"));
                    }
                    else
                    {
                        StartCountDown.UpdateText("GO");
                    }
                }
                else
                {
                    //baja el tiempo del juego
                    _currentGameTime -= T.GetDT();

                    GameTimer.UpdateText(_currentGameTime.ToString("00"));
                }


                break;

            case EstadoJuego.Finalizado:

                //muestra el puntaje

                TiempEspMuestraPts -= Time.deltaTime;
                if (TiempEspMuestraPts <= 0)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                break;
        }

        // TiempoDeJuegoText.transform.parent.gameObject.SetActive(EstAct == EstadoJuego.Jugando && !ConteoRedresivo);
    }
    //----------------------------------------------------------//

    public void IniciarTutorial()
    {
        for (int i = 0; i < ObjsCalibracion1.Length; i++)
        {
            ObjsCalibracion1[i].SetActive(true);
            ObjsCalibracion2[i].SetActive(true);
        }

        for (int i = 0; i < ObjsCarrera.Length; i++)
        {
            ObjsCarrera[i].SetActive(false);
        }

        Player1.CambiarATutorial();

        if (config.isSinglePlayer)
            return;

        Player2.CambiarATutorial();
    }

    void EmpezarCarrera()
    {
        Player1.StartDriving();

        if (config.isSinglePlayer)
            return;

        Player2.StartDriving();
    }

    void FinalizarCarrera()
    {
        EstAct = EstadoJuego.Finalizado;

        _currentGameTime = 0;

        if (Player2 == null)
        {
            DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;
            DatosPartida.PtsGanador = Player1.Dinero;
            Player1.GetComponent<Frenado>().Frenar();
            Player1.ContrDesc.FinDelJuego();
            return;
        }
        else if (Player1.Dinero > Player2.Dinero)
        {
            //lado que gano
            if (Player1.LadoActual == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;
            //puntajes
            DatosPartida.PtsGanador = Player1.Dinero;
            DatosPartida.PtsPerdedor = Player2.Dinero;
        }
        else
        {
            //lado que gano
            if (Player2.LadoActual == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

            //puntajes
            DatosPartida.PtsGanador = Player2.Dinero;
            DatosPartida.PtsPerdedor = Player1.Dinero;
        }

        Player1.GetComponent<Frenado>().Frenar();
        Player2.GetComponent<Frenado>().Frenar();

        Player1.ContrDesc.FinDelJuego();
        Player2.ContrDesc.FinDelJuego();
    }

    //cambia a modo de carrera
    void CambiarACarrera()
    {
        EstAct = EstadoJuego.Jugando;
        ConteoParaInicion = 3;
        ResetTimer();
        ConteoRedresivo = true;

        for (int i = 0; i < ObjsCarrera.Length; i++)
        {
            ObjsCarrera[i].SetActive(true);
        }

        //desactivacion de la calibracion
        Player1.FinCalibrado = true;

        for (int i = 0; i < ObjsCalibracion1.Length; i++)
        {
            ObjsCalibracion1[i].SetActive(false);
        }

        Player1.CambiarAConduccion();

        if (config.isSinglePlayer)
            return;

        Player2.FinCalibrado = true;

        for (int i = 0; i < ObjsCalibracion2.Length; i++)
        {
            ObjsCalibracion2[i].SetActive(false);
        }

        Player2.CambiarAConduccion();

        //Enable counter texts NOT HERE
        //TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        //ConteoInicio.gameObject.SetActive(false);
    }

    public void FinCalibracion(int playerID)
    {
        if (playerID == 0)
        {
            Player1.FinTuto = true;
        }

        if (Player1.FinTuto && config.isSinglePlayer)
        {
            CambiarACarrera();
            return;
        }

        if (playerID == 1)
        {
            Player2.FinTuto = true;
        }

        if (Player1.FinTuto && Player2.FinTuto)
            CambiarACarrera();
    }

    public void SpawnPlayers()
    {
        Player1 = null;
        Player2 = null;

        if (config.isSinglePlayer)
            Player1 = SpawnPlayer(config.PosCamionesCarrera[0], singlePlayerConfig).GetComponent<Player>();
        else
        {
            Player1 = SpawnPlayer(config.PosCamionesCarrera[1], playerOneConfig).GetComponent<Player>();
            Player2 = SpawnPlayer(config.PosCamionesCarrera[2], playerTwoConfig).GetComponent<Player>();
        }

        onToggleUI?.Invoke(true);
    }

    private GameObject SpawnPlayer(Vector3 position, PlayerConfigSO playerConfig)
    {
        GameObject playerObject = Instantiate(playerPrefab, position, Quaternion.identity);
        Player player = playerObject.GetComponent<Player>();
        player.config = playerConfig;

        player.SetConfig();
        return playerObject;
    }

    public bool IsSinglePlayer()
    {
        return config.isSinglePlayer;
    }

    public void ResetTimer()
    {
        _currentGameTime = TiempoDeJuego;
    }
}