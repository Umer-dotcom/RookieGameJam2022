using System.Collections;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake INSTANCE { get; private set; }

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin vCamPerlin;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private void Awake()
    {
        INSTANCE = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

    
    }

    public void ShakeCamera(float intensity, float time)
    {
        vCamPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        StartCoroutine(CamShakeCoroutine(intensity, time));
    }
    
    IEnumerator CamShakeCoroutine(float intensity, float time)
    {
        float shakeTime = time;
        while (shakeTime > 0f)
        {
            shakeTime -= Time.deltaTime;
            vCamPerlin.m_AmplitudeGain = intensity;
            yield return null;
        }
        vCamPerlin.m_AmplitudeGain = 0f;
    }
}
