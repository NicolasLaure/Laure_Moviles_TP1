using UnityEngine;

public class TaxiPool : ObjectPool.ObjectPool
{
    public static TaxiPool instance;

    [Header("Difficulty Config")]
    [SerializeField] private TaxiDifficultyConfig difficultyConfig;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    protected override void Start()
    {
        poolConfig = difficultyConfig.poolConfig;
        base.Start();
    }
}