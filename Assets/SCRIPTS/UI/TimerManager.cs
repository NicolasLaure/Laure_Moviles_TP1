using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void UpdateText(string newText)
    {
        text.text = newText;
    }

    public void ToggleOnOff(bool value)
    {
        gameObject.SetActive(value);
    }
}