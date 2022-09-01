using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(Animator))]
public class KidScript : MonoBehaviour
{

    public int icecreamNeeded = 3;
    

    NavMeshAgent navMeshAgent;
    Vector3 target;
    
    bool hasReachedTarget = false;
    bool hasReachedDestination = false;
    //readonly float minDistanceChange = 1f;
    int wayPointIndex = 0;
    int icecreamHitCount = 0;
    bool kidHappy = false;
    //Animator animator;
    SkinnedMeshRenderer kidRenderer;
    //Vector3 worldDeltaPosition;
    //Vector2 groundDeltaPosition;
    //Vector2 velocity = Vector2.zero;

    Rigidbody[] rigidbodies;
    BlendShapeController blendShapeController;
   

    void Start()
    {
        blendShapeController = GetComponentInChildren<BlendShapeController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
        //animator = GetComponent<Animator>();
        //navMeshAgent.updatePosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        //worldDeltaPosition = navMeshAgent.nextPosition - transform.position;
        //groundDeltaPosition.x = Vector3.Dot(transform.right, worldDeltaPosition);
        //groundDeltaPosition.y = Vector3.Dot(-transform.forward, worldDeltaPosition);

        //velocity = (Time.deltaTime > 1e-5f) ? groundDeltaPosition / Time.deltaTime : velocity = Vector2.zero;
        //bool shouldMove = velocity.magnitude > 0.025f && navMeshAgent.remainingDistance > navMeshAgent.radius;

        //animator.SetBool("move", shouldMove);
        //animator.SetFloat("velx", velocity.x);
        //animator.SetFloat("vely", velocity.y * 3);
        //Debug.Log(velocity);

        if (navMeshAgent.enabled && !hasReachedTarget && !hasReachedDestination) navMeshAgent.destination = target;
        
        //Debug.DrawRay(transform.position, target - transform.position);

    }
    //private void OnAnimatorMove()
    //{
    //    transform.position = navMeshAgent.nextPosition;
    //}
    public BlendShapeController GetBlendShapeController()
    {
        return blendShapeController;
    }
    public void SetTarget(Vector3 targetPosition)
    {
        //if ((targetPosition - target).sqrMagnitude < minDistanceChange * minDistanceChange) return;
        target = targetPosition;
        
        //Debug.Log("Target " + targetPosition);

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
        if (wayPointIndex >= 5) hasReachedDestination = true;
    }
    public bool IsTargetReached()
    {
        return hasReachedTarget;
    }
    public bool IsKidHappy()
    {
        return kidHappy;
    }

    public int GetIcecreamHitCount()
    {
        return icecreamHitCount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Hit by bullet");
            icecreamHitCount++;
            //blendShapeController.MakeFat(icecreamNeeded, icecreamHitCount);
            if (icecreamHitCount >= icecreamNeeded)
            {
                navMeshAgent.enabled = false;
                foreach (Rigidbody rb in rigidbodies)
                {
                    rb.isKinematic = false;
                    
                    blendShapeController.StomachFilled();
                }
                
                kidHappy = true;
                Collider mainCollider = GetComponent<Collider>();
                mainCollider.enabled = false;
                StartCoroutine(TurnKidsOffDelay());
                
;
            }
        }
    }
    IEnumerator TurnKidsOffDelay()
    {
        yield return new WaitForSeconds(3f);
        foreach(Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
        Collider[] allColliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in allColliders)
        {
            collider.enabled = false;
        }
    }
}
