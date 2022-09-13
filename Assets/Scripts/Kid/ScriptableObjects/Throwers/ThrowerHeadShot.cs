using System;
using UnityEngine;

public class ThrowerHeadShot : MonoBehaviour
{
    [SerializeField] GameObject kid;
    private const string bulletTag = "Bullet";

    KidThrowerScript kidThrowerScript;

    private void Awake()
    {
        //kid = GetComponent<KidInfantryScript>();
        kidThrowerScript = kid.GetComponent<KidThrowerScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(bulletTag))
        {
            //OnHeadShot(gameObject.GetComponent<Kid>().GetKidID(), collision);
            
            if (kidThrowerScript.GetHitCount() >= kidThrowerScript.GetHitsToKill() - 3)
            {
                Collider headCollider = GetComponent<Collider>();
                headCollider.enabled = false;
                
            }
            kidThrowerScript.HeadShot(collision);
        }
    }
}

