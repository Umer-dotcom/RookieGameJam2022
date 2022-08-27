using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointScript : MonoBehaviour
{

    public delegate void WayPointEnterDelegate(KidScript kidScript);
    public event WayPointEnterDelegate WayPointEnterEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kid"))
        {
            Debug.Log("ReachedTarget");
            KidScript kidScript = other.GetComponentInChildren<KidScript>();
            if (kidScript != null)
            {
                kidScript.ReachedTarget();
                
                //WayPointEnterEvent?.Invoke(kidScript);
            }
            

        }
    }
}
