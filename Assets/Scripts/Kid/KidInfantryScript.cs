
using System.Collections;
using UnityEngine;
using System;
public enum KidState
{
    NONE,
    CALM,
    NAUGHTY,
    ANGRY
}

public class KidInfantryScript : Kid
{


    public static event Action InfantryKilledEvent = delegate { };

    KidState kidState = KidState.NONE;
    //bool[] kidState;
    static int kidInfantryCount = 0;
    int kidInfantryID = 0;
    BlendShapeController blendShapeController;
    KidMovementScript kidMovementScript;

    int state1;
    int state2;
    int state3;

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
        //poolerScript = ObjectPoolerScript.Instance;
        kidMovementScript = GetComponent<KidMovementScript>();
        ragdollEnabler = GetComponent<RagdollEnabler>();
        poolerScript = ObjectPoolerScript.Instance;
    }

    protected new void Start()
    {
        
        base.Start();
        
        kidInfantryID = kidInfantryCount;
        kidInfantryCount++;
        blendShapeController = GetComponentInChildren<BlendShapeController>();

        state1 = stateTransitionValue;
        state2 = stateTransitionValue * 2;
        state3 = hitsToKill - 1;

    }
    public BlendShapeController GetBlendShapeController()
    {
        return blendShapeController;
    }
    public void HeadShot(Collision collision)
    {
        bool stateChanged = false;
        //if (ID != GetKidID()) return;
        hitCount += 3;
        
        
        if (kidState == KidState.NONE && (hitCount >= state1 && hitCount < state2))
        {
            stateChanged = true;
            kidState = KidState.CALM;
         

        }
        else if (kidState == KidState.CALM && (hitCount >= state2 && hitCount < state3))
        {
            stateChanged = true;
            kidState = KidState.NAUGHTY;
            
        }
        else if (kidState == KidState.NAUGHTY && hitCount >= state3)
        {
            stateChanged = true;
            kidState = KidState.ANGRY;
            
        }

        if (stateChanged)
        {
            switch (kidState)
            {
                case KidState.CALM:
                    kidMovementScript.GetAggressive();

                    poolerScript.SpawnFromPool(OPTag.NAUGHTYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                    break;
                case KidState.NAUGHTY:
                    kidMovementScript.GetAggressive();

                    poolerScript.SpawnFromPool(OPTag.NAUGHTYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                    break;
                case KidState.ANGRY:
                    kidMovementScript.GetAggressive();
                    poolerScript.SpawnFromPool(OPTag.ANGRYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                    break;

            }
        }




        if (hitCount >= hitsToKill)
        {

            ragdollForce = 50f;
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

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            bool stateChanged = false;
            hitCount++;
            
            
            if (kidState == KidState.NONE && (hitCount >= state1 && hitCount < state2))
            {
                stateChanged = true;
                kidState = KidState.CALM;
                

            }
            else if (kidState == KidState.CALM && (hitCount >= state2 && hitCount < state3))
            {
                stateChanged = true;
                kidState = KidState.NAUGHTY;
                
            }
            else if (kidState == KidState.NAUGHTY && hitCount >= state3)
            {
                stateChanged = true;
                kidState = KidState.ANGRY;

            }

            if (stateChanged)
            {
                switch (kidState)
                {
                    case KidState.CALM:
                        kidMovementScript.GetAggressive();

                        poolerScript.SpawnFromPool(OPTag.NAUGHTYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        break;
                    case KidState.NAUGHTY:
                        kidMovementScript.GetAggressive();

                        poolerScript.SpawnFromPool(OPTag.NAUGHTYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        break;
                    case KidState.ANGRY:
                        kidMovementScript.GetAggressive();
                        poolerScript.SpawnFromPool(OPTag.ANGRYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        break;

                }
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
        poolerScript.SpawnFromPool(OPTag.ZZZ, ragdollEnabler.GetRagdollRoot().position + new Vector3(0, 1, 0), Quaternion.identity);
        ragdollEnabler.DisableAllRigidbodies();
    }

}
