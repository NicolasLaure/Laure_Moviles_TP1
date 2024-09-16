using System;
using UnityEngine;
using System.Collections;

public class Deposito2 : MonoBehaviour
{
    Player PjActual;
    public string PlayerTag = "Player";
    public bool Vacio = true;
    public ControladorDeDescarga Contr1;
    public ControladorDeDescarga Contr2;

    [SerializeField] private Transform depositPoint;
    Collider[] PjColl;

    [SerializeField] private ColliderHandler colliderHandler;
    //----------------------------------------------//

    void Start()
    {
        Physics.IgnoreLayerCollision(8, 9, false);

        colliderHandler.onTriggerEnter += HandleTriggerEnter;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Vacio)
        {
            PjActual.transform.position = transform.position;
            PjActual.transform.forward = transform.forward;
        }
    }

    private void OnDisable()
    {
        colliderHandler.onTriggerEnter -= HandleTriggerEnter;
    }

    //----------------------------------------------//
    void HandleTriggerEnter(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            if (Vacio)
            {
                Player player = other.GetComponent<Player>();
                Frenado frenado = other.GetComponent<Frenado>();
                if (player.ConBolasas())
                {
                    Entrar(player);
                    frenado.Destino = depositPoint.position;
                    frenado.transform.forward = frenado.Destino - frenado.transform.position;
                    frenado.Frenar();
                }
            }
        }
    }

    public void Soltar()
    {
        PjActual.VaciarInv();
        PjActual.GetComponent<Frenado>().RestaurarVel();
        PjActual.GetComponent<Respawn>().Respawnear(depositPoint.position, transform.forward);

        PjActual.GetComponent<Rigidbody>().useGravity = true;
        for (int i = 0; i < PjColl.Length; i++)
            PjColl[i].enabled = true;

        Physics.IgnoreLayerCollision(8, 9, false);

        PjActual = null;
        Vacio = true;
    }

    public void Entrar(Player pj)
    {
        if (pj.ConBolasas())
        {
            PjActual = pj;

            PjColl = PjActual.GetComponentsInChildren<Collider>();
            for (int i = 0; i < PjColl.Length; i++)
                PjColl[i].enabled = false;
            PjActual.GetComponent<Rigidbody>().useGravity = false;

            PjActual.transform.position = transform.position;
            PjActual.transform.forward = transform.forward;

            Vacio = false;

            Physics.IgnoreLayerCollision(8, 9, true);

            Entro();
        }
    }

    public void Entro()
    {
        if (PjActual.LadoActual == Visualizacion.Lado.Central || PjActual.LadoActual == Visualizacion.Lado.Izq)
            Contr1.Activar(this);
        else
            Contr2.Activar(this);
    }
}