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

        private List<GameObject> _objects;

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

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].activeInHierarchy)
                    continue;

                _objects[i].SetActive(true);
                return _objects[i];
            }

            throw new Exception("No Inactive Objects on pool");
        }

        public void ReturnObject(GameObject objectToDisable)
        {
            foreach (GameObject _object in _objects)
            {
                if (_object == objectToDisable)
                    _object.SetActive(false);
            }

            throw new Exception("Object Was not part of the pool");
        }
    }
}