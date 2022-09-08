
using System.Collections;
using UnityEngine;

public class KidInfantryScript : Kid
{
    //RagdollEnabler ragdollEnabler;
    static int kidInfantryCount = 0;
    int kidInfantryID = 0;

    KidMovementScript kidMovement;

    
    //Vector3 target;
    BlendShapeController blendShapeController;

    public int GetKidInfantryID()
    {
        return kidInfantryID;
    }
    private void Awake()
    {
        ragdollEnabler = GetComponent<RagdollEnabler>();
        kidMovement = GetComponent<KidMovementScript>();
    }

    protected new void Start()
    {
        
        base.Start();
        
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
                
                //kidMovement.KidDied();
                kidActive = false;
                Rigidbody[] rigidbodies = ragdollEnabler.EnableRagdoll();
                Vector3 forceDirection = -(collision.contacts[0].point - transform.position).normalized;
                forceDirection.y = 0;
                foreach (Rigidbody rb in rigidbodies)
                {

                    rb.AddForce(forceDirection * 100f, ForceMode.Impulse);
                    //rb.isKinematic = false;

                    //blendShapeController.StomachFilled();
                }


                Collider mainCollider = GetComponent<Collider>();
                //foreach (Collider col in colliders)
                //{
                //    col.enabled = true;
                //}
                mainCollider.enabled = false;
                StartCoroutine(TurnKidsOffDelay(3f));


            }

        }
    }
    protected override IEnumerator TurnKidsOffDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        Instantiate(sleepEffect, ragdollEnabler.GetRagdollRoot().position + new Vector3(0, 1, 0), Quaternion.identity);
        ragdollEnabler.DisableAllRigidbodies();
    }

}
