using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class MngPts : MonoBehaviour
{
    Rect R = new Rect();

    public float TiempEmpAnims = 2.5f;
    float Tempo = 0;

    public float TiempEspReiniciar = 10;

    public float TiempParpadeo = 0.7f;
    float TiempoParpadeo = 0;
    bool PrimerImaParp = true;

    public bool ActivadoAnims = false;

    Visualizacion Viz = new Visualizacion();

    [SerializeField] private GameConfig config;
    [SerializeField] private FinalPanel singlePlayerPanel;
    [SerializeField] private FinalPanel multiplayerPanel;
    //---------------------------------//

    // Use this for initialization
    private void Start()
    {
        TurnOnPanel();
        SetGanador();
        SetDinero();
    }

    // Update is called once per frame
    private void Update()
    {
        //PARA JUGAR
        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(1);
        }

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        TiempEspReiniciar -= Time.deltaTime;
        if (TiempEspReiniciar <= 0)
        {
            SceneManager.LoadScene(1);
        }

        if (ActivadoAnims)
        {
            TiempoParpadeo += Time.deltaTime;

            if (TiempoParpadeo >= TiempParpadeo)
            {
                TiempoParpadeo = 0;

                if (PrimerImaParp)
                    PrimerImaParp = false;
                else
                {
                    TiempoParpadeo += 0.1f;
                    PrimerImaParp = true;
                }
            }
        }


        if (!ActivadoAnims)
        {
            Tempo += Time.deltaTime;
            if (Tempo >= TiempEmpAnims)
            {
                Tempo = 0;
                ActivadoAnims = true;
            }
        }
    }

    private void TurnOnPanel()
    {
        if (config.isSinglePlayer)
            singlePlayerPanel.gameObject.SetActive(true);
        else
            multiplayerPanel.gameObject.SetActive(true);
    }
    private void SetGanador()
    {
        if (!config.isSinglePlayer)
        {
            switch (DatosPartida.LadoGanadaor)
            {
                case DatosPartida.Lados.Der:
                    multiplayerPanel.SetWinnerText("PLAYER #2 IS THE WINNER");
                    break;
                case DatosPartida.Lados.Izq:
                    multiplayerPanel.SetWinnerText("PLAYER #1 IS THE WINNER");
                    break;
            }
        }
    }

    private void SetDinero()
    {
        if (!config.isSinglePlayer)
        {
            string leftText;
            string rightText;
            if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq) //izquierda
            {
                leftText = "$" + Viz.PrepararNumeros(DatosPartida.PtsGanador);
                rightText = "$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor);
            }
            else
            {
                leftText = "$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor);
                rightText = "$" + Viz.PrepararNumeros(DatosPartida.PtsGanador);
            }

            multiplayerPanel.SetMoneyTexts(leftText, rightText);
        }
        else
        {
            singlePlayerPanel.SetMoneyText("$" + Viz.PrepararNumeros(DatosPartida.PtsGanador));
        }
    }

    public void DesaparecerGUI()
    {
        ActivadoAnims = false;
        Tempo = -100;
    }
}