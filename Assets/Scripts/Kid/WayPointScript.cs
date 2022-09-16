using System;
using UnityEngine;

public class WayPointScript : MonoBehaviour
{
    public int waypointSourceID;
    public static event Action<int> _onWaypointEnter = delegate { };
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kid"))
        {
            
            KidInfantryScript kidScript = other.GetComponentInChildren<KidInfantryScript>();
            if (kidScript != null)
            {
                
                _onWaypointEnter(kidScript.GetKidInfantryID());
                
            }
            

        }
    }
}
