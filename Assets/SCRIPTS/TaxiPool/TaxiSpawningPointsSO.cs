using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawningPositions", menuName = "Pool/Taxis/SpawningPositions", order = 0)]
public class TaxiSpawningPointsSO : ScriptableObject
{
    public List<Vector3> spawningPositions;
    public List<Vector3> spawningRotationsEuler;
}