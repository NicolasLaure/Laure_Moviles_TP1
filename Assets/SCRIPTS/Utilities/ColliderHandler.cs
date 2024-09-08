using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderHandler : MonoBehaviour
{
    public event Action<GameObject> onTriggerEnter;
    void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(other.gameObject);
    }
}
