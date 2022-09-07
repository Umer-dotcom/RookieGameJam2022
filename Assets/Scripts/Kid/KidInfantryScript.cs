
using UnityEngine;

public class KidInfantryScript : Kid
{
    
    static int kidInfantryCount = 0;
    int kidInfantryID = 0;

    KidMovementScript kidMovement;

    
    //Vector3 target;
    BlendShapeController blendShapeController;

    public int GetKidInfantryID()
    {
        return kidInfantryID;
    }


    protected new void Start()
    {
        
        base.Start();
        kidMovement = GetComponent<KidMovementScript>();
        kidInfantryID = kidInfantryCount;
        kidInfantryCount++;
        blendShapeController = GetComponentInChildren<BlendShapeController>();
        
    }
    public BlendShapeController GetBlendShapeController()
    {
        return blendShapeController;
    }


    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {


            hitCount++;
            Debug.Log(hitCount);
            if (hitCount >= hitsToKill)
            {
                
                kidMovement.KidDied();
                kidActive = false;
                foreach (Rigidbody rb in rigidbodies)
                {
                    rb.isKinematic = false;

                    blendShapeController.StomachFilled();
                }


                Collider mainCollider = GetComponent<Collider>();
                foreach (Collider col in colliders)
                {
                    col.enabled = true;
                }
                mainCollider.enabled = false;
                StartCoroutine(TurnKidsOffDelay(3f));


            }

        }
    }

}
