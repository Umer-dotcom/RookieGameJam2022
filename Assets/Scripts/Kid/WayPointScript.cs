using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kid"))
        {
            Debug.Log("ReachedTarget");
            KidScript kidScript = other.GetComponentInChildren<KidScript>();
            if (kidScript != null)
            {
                kidScript.ReachedTarget();
                
            }
            

        }
    }
}
