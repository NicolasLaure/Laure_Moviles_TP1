using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameConfig", menuName = "GameManagement/GameConfig", order = 0)]
public class GameConfig : ScriptableObject
{
    public bool isSinglePlayer;
    
    //Player positions 0 for singlePlayer and 1,2 for their respective player number
    public Vector3[] PosCamionesCarrera;
}