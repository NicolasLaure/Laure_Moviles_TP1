using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class MngPts : MonoBehaviour
{
    Rect R = new Rect();

    public float TiempEmpAnims = 2.5f;
    float Tempo = 0;

    public TextMeshProUGUI dinero1Text;
    public TextMeshProUGUI dinero2Text;

    public TextMeshProUGUI winnerText;

    public float TiempEspReiniciar = 10;


    public float TiempParpadeo = 0.7f;
    float TiempoParpadeo = 0;
    bool PrimerImaParp = true;

    public bool ActivadoAnims = false;

    Visualizacion Viz = new Visualizacion();

    //---------------------------------//

    // Use this for initialization
    private void Start()
    {
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

    private void SetGanador()
    {
        switch (DatosPartida.LadoGanadaor)
        {
            case DatosPartida.Lados.Der:
                winnerText.text = "Player #2 Is the winner";
                break;
            case DatosPartida.Lados.Izq:
                winnerText.text = "Player #1 Is the winner";
                break;
        }
    }

    private void SetDinero()
    {
        if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq) //izquierda
        {
            dinero1Text.text = "$" + Viz.PrepararNumeros(DatosPartida.PtsGanador);
            dinero2Text.text = "$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor);
        }
        else
        {
            dinero1Text.text = "$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor);
            dinero2Text.text = "$" + Viz.PrepararNumeros(DatosPartida.PtsGanador);
        }
    }

    public void DesaparecerGUI()
    {
        ActivadoAnims = false;
        Tempo = -100;
    }
}