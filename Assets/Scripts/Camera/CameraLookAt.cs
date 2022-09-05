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
            // Make character bones rotate in direction of crosshair
            BodyController.instance.TurnSpine(hit.point);
            //BodyController.instance.TurnNeck(hit.point);

            //Make the kid the gun is pointing at smile
            if (hit.collider.gameObject.CompareTag("Kid"))
            {
                //KidInfantryScript kidInfantryScript = hit.collider.gameObject.GetComponent<KidInfantryScript>();
                //if (kidInfantryScript == null)
                //{
                    
                //}
                //BlendShapeController blendController = kidInfantryScript.GetBlendShapeController();
                //if (!blendController.IsMouthOpen())
                //{
                //    blendController.OpenMouth();
                //}
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.blue);
    }
}
