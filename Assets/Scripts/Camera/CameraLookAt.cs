using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLookAt : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cam;
    [SerializeField]
    private float horizontalSpeed = 300f;
    [SerializeField]
    private float verticalSpeed = 300f;
    [SerializeField]
    private float limitPosY = 45f;
    [SerializeField]
    private float limitNegY = -45f;
    [SerializeField]
    private float limitPosX = 25f;
    [SerializeField]
    private float limitNegX = -10f;

    private void Awake()
    {
        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if(brain != null)
        {
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }

        cam = GetComponent<CinemachineVirtualCamera>();
        cam.Priority = 1;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            LookAt.instance.LookTowards(hit.point); // Make gun look towards the hit point

            if (cam)
            {
                CinemachinePOV pointOfView = cam.GetCinemachineComponent<CinemachinePOV>();

                //if (pointOfView.m_HorizontalAxis.Value >= 0 || pointOfView.m_HorizontalAxis.Value <= 0)
                //    BodyController.instance.TurnSpine(hit.point);
                //if (pointOfView.m_VerticalAxis.Value >= 0 || pointOfView.m_VerticalAxis.Value <= 0)
                //    BodyController.instance.TurnNeck(hit.point);

                if (pointOfView.m_HorizontalAxis.Value >= limitPosY)
                {
                    pointOfView.m_HorizontalAxis.Value = limitPosY;
                }
                if (pointOfView.m_HorizontalAxis.Value <= limitNegY)
                {
                    pointOfView.m_HorizontalAxis.Value = limitNegY;
                }
                if (pointOfView.m_VerticalAxis.Value >= limitPosX)
                {
                    pointOfView.m_VerticalAxis.Value = limitPosX;
                }
                if (pointOfView.m_VerticalAxis.Value <= limitNegX)
                {
                    pointOfView.m_VerticalAxis.Value = limitNegX;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.blue);
    }
}
