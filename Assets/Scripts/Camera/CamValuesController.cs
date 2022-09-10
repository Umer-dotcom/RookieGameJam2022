using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamValuesController : MonoBehaviour
{
    [SerializeField]
    private Debugg config;
    [SerializeField]
    private CinemachineVirtualCamera cam;

    void Update()
    {
        cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = config.camXSensitivity;
        cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = config.camXSensitivity;

    }
}
