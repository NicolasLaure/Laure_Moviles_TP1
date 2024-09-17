using System;
using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;

    void Update()
    {
        fpsText.text = Mathf.FloorToInt(1.0f / Time.deltaTime).ToString();
    }
}