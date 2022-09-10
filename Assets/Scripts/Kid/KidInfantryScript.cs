
using System.Collections;
using UnityEngine;

public class KidInfantryScript : Kid
{
    ObjectPoolerScript poolerScript;
    static int kidInfantryCount = 0;
    int kidInfantryID = 0;
    BlendShapeController blendShapeController;
    KidMovementScript kidMovementScript;

    float ragdollForce = 25f;
    private void OnEnable()
    {
        //HeadShotScript.OnHeadShot += HeadShot;
    }
    private void OnDisable()
    {
        //HeadShotScript.OnHeadShot -= HeadShot;
    }
    public int GetKidInfantryID()
    {
        return kidInfantryID;
    }
    private void Awake()
    {
        poolerScript = ObjectPoolerScript.Instance;
        kidMovementScript = GetComponent<KidMovementScript>();
        ragdollEnabler = GetComponent<RagdollEnabler>();
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
    public void HeadShot(Collision collision)
    {
        Debug.Log("HeadShot!!");
        //if (ID != GetKidID()) return;
        hitCount += 3;
        //int state1 = stateTransitionValue;
        //int state2 = stateTransitionValue * 2;
        //int state3 = hitsToKill - 1;
        //poolerScript.SpawnFromPool(OPTag.CRIT, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        kidMovementScript.GetAggressive();
        //if (hitCount == state1)
        //{
        //    kidMovementScript.GetAggressive();
        //    poolerScript.SpawnFromPool(OPTag.NAUGHTYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);

        //}
        //else if (hitCount == state2)
        //{
        //    kidMovementScript.GetAggressive();

        //    poolerScript.SpawnFromPool(OPTag.NAUGHTYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        //}
        //else if (hitCount == state3)
        //{
        //    kidMovementScript.GetAggressive();
        //    poolerScript.SpawnFromPool(OPTag.ANGRYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        //}




        if (hitCount >= hitsToKill)
        {

            ragdollForce = 50f;
            kidActive = false;
            Rigidbody[] rigidbodies = ragdollEnabler.EnableRagdoll();
            Vector3 forceDirection = -(collision.contacts[0].point - transform.position).normalized;
            forceDirection.y = 0;
            //collision.gameObject.GetComponent<Collider>().enabled = false;
            //collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            foreach (Rigidbody rb in rigidbodies)
            {

                rb.AddForce(forceDirection * ragdollForce, ForceMode.Impulse);

            }
            


            Collider mainCollider = GetComponent<Collider>();
            mainCollider.enabled = false;
            StartCoroutine(TurnKidsOffDelay(3f));


        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {


            hitCount++;
            int state1 = stateTransitionValue;
            int state2 = stateTransitionValue * 2;
            int state3 = hitsToKill - 1;

            if (hitCount == state1)
            {
                kidMovementScript.GetAggressive();
                poolerScript.SpawnFromPool(OPTag.NAUGHTYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);

            } else if (hitCount == state2)
            {
                kidMovementScript.GetAggressive();

                poolerScript.SpawnFromPool(OPTag.NAUGHTYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            } else if (hitCount == state3)
            {
                kidMovementScript.GetAggressive();
                poolerScript.SpawnFromPool(OPTag.ANGRYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            }
            


            if (hitCount >= hitsToKill)
            {
                
                
                kidActive = false;
                Rigidbody[] rigidbodies = ragdollEnabler.EnableRagdoll();
                Vector3 forceDirection = -(collision.contacts[0].point - transform.position).normalized;
                forceDirection.y = 0;
                
                foreach (Rigidbody rb in rigidbodies)
                {

                    rb.AddForce(forceDirection * ragdollForce, ForceMode.Impulse);

                }


                Collider mainCollider = GetComponent<Collider>();
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
