using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "GameManagement/GameConfig", order = 0)]
public class GameConfig : ScriptableObject
{
    public bool isTwoPlayer;
}