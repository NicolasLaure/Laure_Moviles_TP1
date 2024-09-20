using System;
using System.Collections.Generic;
using UnityEngine;
using BagsPool;

public class BagsSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPositions = new List<Transform>();

    private int _currentIndex = 0;

    private void Awake()
    {
        _currentIndex = 0;
    }

    private void Start()
    {
        for (int i = 0; i < BagPool.instance.Count; i++)
        {
            HandleSpawnBag();
        }
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