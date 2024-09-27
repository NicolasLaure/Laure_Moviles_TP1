using UnityEngine;

public class TaxiPool : ObjectPool.ObjectPool
{
    [Header("Difficulty Config")]
    [SerializeField] private TaxiDifficultyConfig difficultyConfig;

    protected override void Start()
    {
        poolConfig = difficultyConfig.poolConfig;
        base.Start();
    }
}