using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLookAt : MonoBehaviour
{
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity))
        {
            LookAt.instance.LookTowards(hit.point); // Make gun look towards the hit point
            ProjectileShooter.instance.SpawnPosLookAt(hit.point);
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.blue);
    }
}
