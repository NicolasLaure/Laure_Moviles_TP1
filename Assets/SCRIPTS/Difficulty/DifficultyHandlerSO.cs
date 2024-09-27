using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "DifficultyHandler", menuName = "Difficulty/Handler", order = 0)]
public class DifficultyHandlerSO : ScriptableObject
{
    [SerializeField] private TaxiDifficultyConfig _taxiDifficultyConfig;

    [SerializeField] private DifficultySO currentDifficulty;

    public void SetDifficulty(DifficultySO difficultySo)
    {
        currentDifficulty = difficultySo;
        _taxiDifficultyConfig.poolConfig = currentDifficulty.taxiPoolConfig;
        _taxiDifficultyConfig.pointsConfig = currentDifficulty.taxiSpawningPointsSo;
    }

    public DifficultySO GetCurrentDifficulty()
    {
        return currentDifficulty;
    }
}