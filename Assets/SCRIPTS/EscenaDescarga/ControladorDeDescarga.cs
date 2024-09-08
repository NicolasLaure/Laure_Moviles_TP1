using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class ControladorDeDescarga : MonoBehaviour
{
    System.Collections.Generic.List<Pallet.Valores> Ps = new System.Collections.Generic.List<Pallet.Valores>();

    int Contador = 0;

    Deposito2 Dep;

    public GameObject[] Componentes; //todos los componentes que debe activar en esta escena

    public Player player; //jugador
    MeshCollider CollCamion;

    public Pallet PEnMov = null;

    //los prefab de los pallets
    public GameObject Pallet1;
    public GameObject Pallet2;
    public GameObject Pallet3;


    public Estanteria Est1;
    public Estanteria Est2;
    public Estanteria Est3;

    public Cinta Cin2;

    public float Bonus = 0;
    float TempoBonus;


    public AnimMngDesc ObjAnimado;


    //--------------------------------------------------------------//

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Componentes.Length; i++)
        {
            Componentes[i].SetActive(false);
        }

        if (ObjAnimado != null)
            ObjAnimado.ContrDesc = this;
    }

    // Update is called once per frame
    void Update()
    {
        //contador de tiempo
        if (PEnMov != null)
        {
            if (TempoBonus > 0)
            {
                Bonus = (TempoBonus * (float)PEnMov.Valor) / PEnMov.Tiempo;
                TempoBonus -= T.GetDT();
            }
            else
            {
                Bonus = 0;
            }
        }
    }

    //--------------------------------------------------------------//
    public void SetPlayer(Player player)
    {
        this.player = player;

        CollCamion = player.GetComponentInChildren<MeshCollider>();
        player.SetContrDesc(this);
    }

    public void Activar(Deposito2 d)
    {
        Dep = d; //recibe el deposito para que sepa cuando dejarlo ir al camion

        //activa los componentes
        for (int i = 0; i < Componentes.Length; i++)
        {
            Componentes[i].SetActive(true);
        }


        CollCamion.enabled = false;
        player.CambiarADescarga();


        GameObject go;
        //asigna los pallets a las estanterias
        for (int i = 0; i < player.Bolasas.Length; i++)
        {
            if (player.Bolasas[i] != null)
            {
                Contador++;

                switch (player.Bolasas[i].Monto)
                {
                    case Pallet.Valores.Valor1:
                        go = Instantiate(Pallet1);
                        Est1.Recibir(go.GetComponent<Pallet>());
                        break;

                    case Pallet.Valores.Valor2:
                        go = (GameObject)Instantiate(Pallet2);
                        Est2.Recibir(go.GetComponent<Pallet>());
                        break;

                    case Pallet.Valores.Valor3:
                        go = (GameObject)Instantiate(Pallet3);
                        Est3.Recibir(go.GetComponent<Pallet>());
                        break;
                }
            }
        }

        //animacion
        ObjAnimado.Entrar();
    }

    //cuando sale de un estante
    public void SalidaPallet(Pallet p)
    {
        PEnMov = p;
        TempoBonus = p.Tiempo;
        player.SacarBolasa();
        //inicia el contador de tiempo para el bonus
    }

    //cuando llega a la cinta
    public void LlegadaPallet(Pallet p)
    {
        //termina el contador y suma los pts

        //termina la descarga
        PEnMov = null;
        Contador--;

        player.Dinero += (int)Bonus;

        if (Contador <= 0)
        {
            Finalizacion();
        }
        else
        {
            Est2.EncenderAnim();
        }
    }

    //metodo llamado por el GameManager para avisar que se termino el juego
    public void FinDelJuego()
    {
        //desactiva lo que da y recibe las bolsas para que no halla mas flujo de estas
        Est2.enabled = false;
        Cin2.enabled = false;
    }

    void Finalizacion()
    {
        ObjAnimado.Salir();
    }

    public Pallet GetPalletEnMov()
    {
        return PEnMov;
    }

    public void FinAnimEntrada()
    {
        //avisa cuando termino la animacion para que prosiga el juego
        Est2.EncenderAnim();
    }

    public void FinAnimSalida()
    {
        //avisa cuando termino la animacion para que prosiga el juego

        for (int i = 0; i < Componentes.Length; i++)
        {
            Componentes[i].SetActive(false);
        }

        CollCamion.enabled = true;

        player.CambiarAConduccion();

        Dep.Soltar();
    }
}