using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player/Config", order = 0)]
public class PlayerConfigSO : ScriptableObject
{
    public ControlDireccion.TipoInput input;
    public Visualizacion.Lado side;

    public Camera calibrationCam;
    public Camera downloadCam;
}