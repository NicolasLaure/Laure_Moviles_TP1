using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Pool/DifficultyConfig", order = 0)]
public class TaxiDifficultyConfig : ScriptableObject
{
    public PoolConfigSO poolConfig;
    public TaxiSpawningPointsSO pointsConfig;
}
