using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class FovBasedOnScreenSize : MonoBehaviour
{
    [SerializeField] private float fieldOfViewAngle;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        virtualCamera.m_Lens.FieldOfView = GetVerticalFov();
    }


    private float GetVerticalFov()
    {
        float width = Screen.width;
        float height = Screen.height;

        float ratio = width / height;

        return fieldOfViewAngle / ratio;
    }
}