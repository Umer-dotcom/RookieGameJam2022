using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseAreaScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kid"))
        {
            Debug.Log("kid entered lose");
            KidMovementScript kidScript = other.GetComponent<KidMovementScript>();
            kidScript.OnEnterDanger();
        } 
         
    }
}
