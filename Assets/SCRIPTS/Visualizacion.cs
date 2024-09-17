using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// clase encargada de TODA la visualizacion
/// de cada player, todo aquello que corresconda a 
/// cada seccion de la pantalla independientemente
/// </summary>
public class Visualizacion : MonoBehaviour
{
    public enum Lado
    {
        Izq,
        Central,
        Der
    }

    public Lado LadoAct;

    ControlDireccion Direccion;
    Player Pj;

    //public GameObject uiRoot;
    private EnableInPlayerState[] enableInPlayerStates;

    //las distintas camaras
    [SerializeField] private Camera _CamConduccion;
    private Camera _CamCalibracion;
    private Camera _CamDescarga;

    //PARA EL INVENTARIO
    public float Parpadeo = 0.8f;
    public float TempParp = 0;
    public bool PrimIma = true;
    public Sprite[] InvSprites;

    [SerializeField] private PlayerConfigSO _config;

    // Use this for initialization
    void Start()
    {
        Direccion = GetComponent<ControlDireccion>();
        Pj = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_config.ui == null)
            return;

        switch (Pj.EstAct)
        {
            case Player.Estados.EnConduccion:
                //inventario
                SetInv();
                //contador de dinero
                SetDinero();
                //el volante
                SetVolante();
                break;

            case Player.Estados.EnDescarga:
                //inventario
                SetInv();
                //el bonus
                SetBonus();
                //contador de dinero
                SetDinero();
                break;

            case Player.Estados.EnTutorial:
                SetTuto();
                break;
        }
    }

    //--------------------------------------------------------//
    public void SetConfig(PlayerConfigSO config)
    {
        _config = config;
        LadoAct = _config.side;
        SetCamerasViewPort(_config.calibrationCam, _config.downloadCam);
    }

    public void CambiarATutorial()
    {
        _CamCalibracion.enabled = true;
        _CamConduccion.enabled = false;
        _CamDescarga.enabled = false;

        // Array.ForEach(enableInPlayerStates, e => e.SetPlayerState(Pj.EstAct));
    }

    public void CambiarAConduccion()
    {
        _CamCalibracion.enabled = false;
        _CamConduccion.enabled = true;
        _CamDescarga.enabled = false;

        //Array.ForEach(enableInPlayerStates, e => e.SetPlayerState(Pj.EstAct));
    }

    public void CambiarADescarga()
    {
        _CamCalibracion.enabled = false;
        _CamConduccion.enabled = false;
        _CamDescarga.enabled = true;
    }

    //---------//

    public void SetLado(Lado lado)
    {
        LadoAct = lado;

        Rect r = new Rect();
        r.width = _CamConduccion.rect.width;
        r.height = _CamConduccion.rect.height;
        r.y = _CamConduccion.rect.y;

        switch (lado)
        {
            case Lado.Der:
                r.x = 0.5f;
                break;


            case Lado.Izq:
                r.x = 0;
                break;
        }

        _CamCalibracion.rect = r;
        _CamConduccion.rect = r;
        _CamDescarga.rect = r;
    }

    void SetBonus()
    {
        if (Pj.ContrDesc.PEnMov != null)
        {
            _config.ui.BonusRoot.SetActive(true);

            //el fondo
            float bonus = Pj.ContrDesc.Bonus;
            float max = (float)(int)Pallet.Valores.Valor1;
            float t = bonus / max;
            _config.ui.BonusFill.fillAmount = t;
            //la bolsa
            _config.ui.BonusText.text = "$" + Pj.ContrDesc.Bonus.ToString("0");
        }
        else
        {
            _config.ui.BonusRoot.SetActive(false);
        }
    }

    void SetDinero()
    {
        _config.ui.Dinero.text = PrepararNumeros(Pj.Dinero);
    }

    void SetTuto()
    {
        switch (Pj.ContrCalib.EstAct)
        {
            case ContrCalibracion.Estados.Calibrando:
                _config.ui.TutoCalibrando.SetActive(true);
                _config.ui.TutoDescargando.SetActive(false);
                _config.ui.TutoFinalizado.SetActive(false);
                break;

            case ContrCalibracion.Estados.Tutorial:
                _config.ui.TutoCalibrando.SetActive(false);
                _config.ui.TutoDescargando.SetActive(true);
                _config.ui.TutoFinalizado.SetActive(false);
                break;

            case ContrCalibracion.Estados.Finalizado:
                _config.ui.TutoCalibrando.SetActive(false);
                _config.ui.TutoDescargando.SetActive(false);
                _config.ui.TutoFinalizado.SetActive(true);
                break;
        }
    }

    void SetVolante()
    {
        float angulo = -45 * Direccion.GetGiro();
        Vector3 rot = _config.ui.Volante.localEulerAngles;
        rot.z = angulo;
        _config.ui.Volante.localEulerAngles = rot;
    }

    void SetInv()
    {
        int contador = 0;
        for (int i = 0; i < 3; i++)
        {
            if (Pj.Bolasas[i] != null)
                contador++;
        }

        if (contador >= 3)
        {
            TempParp += T.GetDT();

            if (TempParp >= Parpadeo)
            {
                TempParp = 0;
                if (PrimIma)
                    PrimIma = false;
                else
                    PrimIma = true;


                if (PrimIma)
                {
                    _config.ui.Inventario.sprite = InvSprites[3];
                }
                else
                {
                    _config.ui.Inventario.sprite = InvSprites[4];
                }
            }
        }
        else
        {
            _config.ui.Inventario.sprite = InvSprites[contador];
        }
    }

    public string PrepararNumeros(int dinero)
    {
        string strDinero = dinero.ToString();
        string res = "";

        if (dinero < 1) //sin ditero
        {
            res = "";
        }
        else if (strDinero.Length == 6) //cientos de miles
        {
            for (int i = 0; i < strDinero.Length; i++)
            {
                res += strDinero[i];

                if (i == 2)
                {
                    res += ".";
                }
            }
        }
        else if (strDinero.Length == 7) //millones
        {
            for (int i = 0; i < strDinero.Length; i++)
            {
                res += strDinero[i];

                if (i == 0 || i == 3)
                {
                    res += ".";
                }
            }
        }

        return res;
    }

    public void SetCamerasViewPort(Camera calibrationCam, Camera downloadCam)
    {
        _CamCalibracion = calibrationCam;
        _CamDescarga = downloadCam;

        switch (LadoAct)
        {
            case Lado.Izq:
                _CamCalibracion.rect = new Rect(0, 0, 0.5f, 1);
                _CamConduccion.rect = new Rect(0, 0, 0.5f, 1);
                _CamDescarga.rect = new Rect(0, 0, 0.5f, 1);
                break;

            case Lado.Central:
                _CamCalibracion.rect = new Rect(0, 0, 1, 1);
                _CamConduccion.rect = new Rect(0, 0, 1, 1);
                _CamDescarga.rect = new Rect(0, 0, 1, 1);
                break;

            case Lado.Der:
                _CamCalibracion.rect = new Rect(0.5f, 0, 0.5f, 1);
                _CamConduccion.rect = new Rect(0.5f, 0, 0.5f, 1);
                _CamDescarga.rect = new Rect(0.5f, 0, 0.5f, 1);
                break;
        }
    }
}