using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player/Config", order = 0)]
public class PlayerConfigSO : ScriptableObject
{
    public ControlDireccion.TipoInput input;
    public Visualizacion.Lado side;

    public Camera calibrationCam;
    public Camera downloadCam;

    public PlayerUI ui;

    public Sprite[] trucks;

    public Vector3EventChannelSO onlastPositionChannel;
}