using UnityEngine;

public class Frenado : MonoBehaviour
{
    public float VelEntrada = 0;
    public string TagDeposito = "Deposito";

    int Contador = 0;
    int CantMensajes = 10;
    float TiempFrenado = 0.5f;
    float Tempo = 0f;

    private Vector3 _destino;

    public bool Frenando = false;

    public Vector3 Destino
    {
        get { return _destino; }
        set { _destino = value; }
    }
    //-----------------------------------------------------//

    // Use this for initialization
    void Start()
    {
        Frenar();
    }

    void FixedUpdate()
    {
        if (Frenando)
        {
            Tempo += T.GetFDT();
            if (Tempo >= (TiempFrenado / CantMensajes) * Contador)
            {
                Contador++;
            }
        }
    }

    //-----------------------------------------------------------//

    public void Frenar()
    {
        GetComponent<ControlDireccion>().enabled = false;
        gameObject.GetComponent<CarController>().SetAcel(0f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        Frenando = true;
        Tempo = 0;
        Contador = 0;
    }

    public void RestaurarVel()
    {
        GetComponent<ControlDireccion>().enabled = true;
        gameObject.GetComponent<CarController>().SetAcel(1f);
        Frenando = false;
        Tempo = 0;
        Contador = 0;
    }
}