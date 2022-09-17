using System;
using UnityEngine;

public class WayPointScript : MonoBehaviour
{
    public KidSpawnerScript wayPointSpawner;
    public static event Action<int> _onWaypointEnter = delegate { };
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kid"))
        {
            
            KidMovementScript kidScript = other.GetComponentInChildren<KidMovementScript>();
            if (kidScript != null && kidScript.spawner.spawnerID == wayPointSpawner.spawnerID)
            {

                //_onWaypointEnter(kidScript.GetKidInfantryID());
                kidScript.spawner.SetKidTarget(kidScript.GetID());
                
            }
            

        }
    }
}
