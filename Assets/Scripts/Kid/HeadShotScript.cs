using System;
using UnityEngine;

public class HeadShotScript : MonoBehaviour
{
    [SerializeField] GameObject kid;
    private const string bulletTag = "Bullet";
    public static event Action<int, Collision> OnHeadShot = delegate { };

    KidInfantryScript kidInfantryScript;
    
    private void Awake()
    {
        //kid = GetComponent<KidInfantryScript>();
        kidInfantryScript = kid.GetComponent<KidInfantryScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(bulletTag))
        {
            //OnHeadShot(gameObject.GetComponent<Kid>().GetKidID(), collision);
            
            if (kidInfantryScript.GetHitCount() >= kidInfantryScript.GetHitsToKill() - 3)
            {
                Collider headCollider = GetComponent<Collider>();
                headCollider.enabled = false;
            }
            kidInfantryScript.HeadShot(collision);
        }
    }
}
