using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BagsPool;

public class BagsSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPositions = new List<Transform>();
    [SerializeField] private VoidEventChannelSO onBagDespawnedEvent;

    private int _currentIndex = 0;

    private void Start()
    {
        StartCoroutine(Initialize());
    }

    private void OnDestroy()
    {
        if (onBagDespawnedEvent != null)
            onBagDespawnedEvent.onVoidEvent -= HandleSpawnBag;
    }

    private IEnumerator Initialize()
    {
        yield return null;
        _currentIndex = 0;
        for (int i = 0; i < BagPool.instance.Count; i++)
        {
            HandleSpawnBag();
        }

        onBagDespawnedEvent.onVoidEvent += HandleSpawnBag;
    }

    void HandleSpawnBag()
    {
        if (_currentIndex >= spawnPositions.Count)
            return;

        if (BagPool.instance.TryGetPooledObject(out GameObject bag))
        {
            bag.transform.position = spawnPositions[_currentIndex].position;
            _currentIndex++;
        }
    }
}