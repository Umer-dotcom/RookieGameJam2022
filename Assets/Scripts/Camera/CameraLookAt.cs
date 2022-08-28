using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLookAt : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            LookAt.instance.LookTowards(hit.point); // Make gun look towards the hit point
            ProjectileShooter.instance.SpawnPosLookAt(hit.point);
            // Make character bones rotate in direction of crosshair
            BodyController.instance.TurnSpine(hit.point);
            BodyController.instance.TurnNeck(hit.point);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.blue);
    }
}
