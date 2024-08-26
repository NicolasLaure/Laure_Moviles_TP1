using UnityEngine;

public class ContrCalibracion : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManagerSo;
    [SerializeField] private int playerNumber;
    public float TiempEspCalib = 3;
    float Tempo2 = 0;

    public enum Estados
    {
        Calibrando,
        Tutorial,
        Finalizado
    }

    public Estados EstAct = Estados.Calibrando;

    public ManejoPallets Partida;
    public ManejoPallets Llegada;
    public Pallet P;
    public ManejoPallets palletsMover;

    //----------------------------------------------------//

    // Use this for initialization
    void Start()
    {
        palletsMover.enabled = false;

        P.CintaReceptora = Llegada.gameObject;
        Partida.Recibir(P);

        SetActivComp(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (EstAct == ContrCalibracion.Estados.Calibrando)
        {
            IniciarTesteo();
        }

        if (EstAct == ContrCalibracion.Estados.Tutorial)
        {
            if (Tempo2 < TiempEspCalib)
            {
                Tempo2 += T.GetDT();
                if (Tempo2 > TiempEspCalib)
                {
                    SetActivComp(true);
                }
            }
        }
    }

    //----------------------------------------------------//

    void IniciarTesteo()
    {
        EstAct = Estados.Tutorial;
        palletsMover.enabled = true;
    }

    public void FinTutorial()
    {
        EstAct = Estados.Finalizado;
        palletsMover.enabled = false;
        gameManagerSo.FinCalibracion(playerNumber);
    }

    void SetActivComp(bool estado)
    {
        if (Partida.GetComponent<Renderer>() != null)
            Partida.GetComponent<Renderer>().enabled = estado;
        Partida.GetComponent<Collider>().enabled = estado;
        if (Llegada.GetComponent<Renderer>() != null)
            Llegada.GetComponent<Renderer>().enabled = estado;
        Llegada.GetComponent<Collider>().enabled = estado;
        P.GetComponent<Renderer>().enabled = estado;
    }
}