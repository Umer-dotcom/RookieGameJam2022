using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
//[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(Animator))]
public class KidInfantryScript : Kid
{
    

    NavMeshAgent navMeshAgent;
    Vector3 target;
    static int kidInfantryCount = 0;
    int kidInfantryID = 0;
    bool hasReachedTarget = false;
    bool hasReachedDestination = false;

    int wayPointIndex = 0;
    
    BlendShapeController blendShapeController;

    public int GetKidInfantryID()
    {
        return kidInfantryID;
    }


    protected new void Start()
    {
        base.Start();
        kidInfantryID = kidInfantryCount;
        kidInfantryCount++;
        blendShapeController = GetComponentInChildren<BlendShapeController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.enabled && !hasReachedTarget && !hasReachedDestination) navMeshAgent.destination = target;
        
        Debug.DrawRay(transform.position, target - transform.position);
    }

    public BlendShapeController GetBlendShapeController()
    {
        return blendShapeController;
    }
    public void SetTarget(Vector3 targetPosition)
    {
        target = targetPosition;

        hasReachedTarget = false;
    }
    public void ReachedTarget(int ID)
    {
        
        if (kidInfantryID == ID && !hasReachedTarget)
        {
            hasReachedTarget = true;
            wayPointIndex++;
            if (wayPointIndex >= 5) hasReachedDestination = true;
        }
    }
    
    public int GetWPIndex() 
    {
        return wayPointIndex;
    }

    public bool IsTargetReached()
    {
        return hasReachedTarget;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {


            hitCount++;
            Debug.Log(hitCount);
            if (hitCount >= hitsToKill)
            {
                navMeshAgent.enabled = false;
                kidActive = true;
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
