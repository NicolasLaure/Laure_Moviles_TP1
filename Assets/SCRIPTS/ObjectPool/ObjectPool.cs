using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool instance;
        [SerializeField] private PoolConfigSO poolConfig;
        [SerializeField] private int count;
        [SerializeField] private VoidEventChannelSO onBagDespawnedEvent;

        private List<GameObject> _objects;

        public int Count => count;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
        }

        private void Start()
        {
            _objects = new List<GameObject>();

            int configObjectIndex = 0;
            for (int i = 0; i < count; i++)
            {
                if (configObjectIndex >= poolConfig.poolObjects.Count)
                    configObjectIndex = 0;

                GameObject instantiatedObject = GameObject.Instantiate(poolConfig.poolObjects[configObjectIndex].prefab);
                instantiatedObject.SetActive(false);
                _objects.Add(instantiatedObject);
                configObjectIndex++;
            }
        }

        public bool TryGetPooledObject(out GameObject pooledObject)
        {
            pooledObject = null;
            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].activeInHierarchy)
                    continue;

                _objects[i].SetActive(true);
                pooledObject = _objects[i];
                return true;
            }

            return false;
        }

        public bool TryReturnObject(GameObject objectToDisable)
        {
            if (_objects.Contains(objectToDisable))
            {
                foreach (GameObject _object in _objects)
                {
                    if (_object == objectToDisable)
                    {
                        _object.SetActive(false);
                        onBagDespawnedEvent.RaiseEvent();
                        return true;
                    }
                }
            }

            return false;
        }
    }
}