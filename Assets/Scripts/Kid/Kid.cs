using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
    [SerializeField] protected int hitsToKill;

    private static int kidCount = 0;

    
    protected int hitCount = 0;
    private int kidID;
    protected bool kidActive = true;
    protected Rigidbody[] rigidbodies;
    protected Collider[] colliders;

    public virtual void Start()
    {
        kidID = kidCount;
        kidCount++;
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach(Collider col in colliders)
        {
            col.enabled = false;
        }
        Collider mainCol = GetComponent<Collider>();
        mainCol.enabled = true;
    }

    public int GetHitsToKill()
    {
        return hitsToKill;
    }
    public int GetHitCount()
    {
        return hitCount;
    }

    public int GetTotalKidCount()
    {
        return kidCount;
    }
    public int GetKidID()
    {
        return kidID;
    }
    public bool IsKidActive()
    {
        return kidActive;
    }
    //protected virtual void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Bullet"))
    //    {

    //        hitCount++;
    //        if (hitCount >= hitsToKill)
    //        {
    //            foreach (Rigidbody rb in rigidbodies)
    //            {
    //                rb.isKinematic = false;
    //            }

    //            kidActive = false;
    //            Collider mainCollider = GetComponent<Collider>();
    //            mainCollider.enabled = false;
    //            StartCoroutine(TurnKidsOffDelay(3f));
    //        }
    //    }
    //}

    protected IEnumerator TurnKidsOffDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        foreach (Rigidbody rb in rigidbodies)
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
