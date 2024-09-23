using Unity.Mathematics;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
#if UNITY_ANDROID || PLATFORM_ANDROID
    void Start()
    {
        Application.targetFrameRate = Mathf.FloorToInt((float)Screen.currentResolution.refreshRateRatio.value);
    }
#endif
}