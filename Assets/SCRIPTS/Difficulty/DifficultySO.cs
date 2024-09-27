using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "Difficulty/Difficulty", order = 0)]
public class DifficultySO : ScriptableObject
{
    public string name;
    public PoolConfigSO taxiPoolConfig;
    public TaxiSpawningPointsSO taxiSpawningPointsSo;
}