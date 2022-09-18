using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseAreaScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kid"))
        {
            
            KidMovementScript kidScript = other.GetComponent<KidMovementScript>();
            kidScript.OnEnterDanger();
        } 
         
    }
}
