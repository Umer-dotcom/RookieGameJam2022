using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class KidScript : MonoBehaviour
{

    public int icecreamNeeded = 3;
    public OPTag objectPoolTag;
    

    NavMeshAgent navMeshAgent;
    Vector3 target;
    
    bool hasReachedTarget = false;
    //readonly float minDistanceChange = 1f;
    int wayPointIndex = 0;
    int icecreamHitCount = 0;
    bool kidHappy = false;

    public delegate void ReachedTargetDelegate(KidScript kidScript);
    public event ReachedTargetDelegate ReachedTargetEvent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = target;
        Debug.DrawRay(transform.position, target - transform.position);

    }
    public void SetTarget(Vector3 targetPosition)
    {
        if ((targetPosition - target).sqrMagnitude < minDistanceChange * minDistanceChange) return;
        target = targetPosition;

        Debug.Log("Target " + targetPosition);
        
        hasReachedTarget = false;
    }
    public void ReachedTarget()
    {
        if (!hasReachedTarget)
        {
            hasReachedTarget = true;
            IncrementWPIndex();
        }
    }
    public int GetWPIndex() 
    {
        return wayPointIndex;
    }
    public void IncrementWPIndex()
    {
        wayPointIndex++;
    }
    public bool IsTargetReached()
    {
        return hasReachedTarget;
    }
    public bool IsKidHappy()
    {
        return kidHappy;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Hit by bullet");
            icecreamHitCount++;
            if (icecreamHitCount >= icecreamNeeded)
            {
                kidHappy = true;
            }
        }
    //    if (other.gameObject.CompareTag("WayPoint"))
    //    {
    //        Debug.Log("ReachedTarget");
            
    //        if (ReachedTargetEvent == null) return;
    //        ReachedTargetEvent(this);
    //        IncrementWPIndex();
    //        if (!hasReachedTarget) hasReachedTarget = true;
    //    }

    }
}
