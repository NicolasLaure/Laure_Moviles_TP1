using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiSpawner : MonoBehaviour
{
    [SerializeField] private TaxiDifficultyConfig difficultyConfig;
    [SerializeField] private VoidEventChannelSO onTaxiDespawnedEvent;

    private int _currentIndex = 0;

    private TaxiSpawningPointsSO _spawningPoints;
    private void Start()
    {
        _spawningPoints = difficultyConfig.pointsConfig;
        StartCoroutine(Initialize());
    }

    private void OnDestroy()
    {
        if (onTaxiDespawnedEvent != null)
            onTaxiDespawnedEvent.onVoidEvent -= HandleSpawnBag;
    }

    private IEnumerator Initialize()
    {
        yield return null;
        _currentIndex = 0;
        for (int i = 0; i < TaxiPool.instance.Count; i++)
        {
            HandleSpawnBag();
        }

        onTaxiDespawnedEvent.onVoidEvent += HandleSpawnBag;
    }

    void HandleSpawnBag()
    {
        if (_currentIndex >= _spawningPoints.spawningPositions.Count)
            return;

        if (TaxiPool.instance.TryGetPooledObject(out GameObject taxi))
        {
            taxi.transform.localPosition = _spawningPoints.spawningPositions[_currentIndex];
            taxi.transform.localRotation = Quaternion.Euler(_spawningPoints.spawningRotationsEuler[_currentIndex]);
            _currentIndex++;
        }
    }
}