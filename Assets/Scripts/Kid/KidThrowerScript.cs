using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KidThrowerScript : Kid
{
    [Header("Throwing")]
    [SerializeField] private GameObject snowPrefab;
    [SerializeField] private Transform snowSpawn;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject projectilesContainer;
    [SerializeField] private KidThrowerSO sO;



    float timeInHidePhase = 0;
    int cycleThrowCount = 0;
    bool hiding = true;
    Animator animator;

    protected new void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        animator.SetFloat("speed", sO.SpeedMultiplier);
    }
    void Update()
    {

        if (!kidActive) return;

        if (!hiding)
        {


            if (cycleThrowCount < sO.ThrowsPerCycle) animator.SetTrigger("throw");
            else Hide();

        } else
        {
            //hiding code

            timeInHidePhase += Time.deltaTime;
            if (timeInHidePhase >= sO.HidePhaseDuration) StopHiding();
        }
    }
    void Hide()
    {
        cycleThrowCount = 0;
        hiding = true;
        animator.SetBool("hiding", hiding);
    }
    void StopHiding()
    {
        hiding = false;
        timeInHidePhase = 0;
        animator.SetBool("hiding", hiding);
    }
    void CreateTheBallAndThrow()
    {
        //Called when the throw animation reaches a specific frame
        GameObject projectile = Instantiate(snowPrefab, snowSpawn);
        projectile.transform.parent = projectilesContainer.transform;
        projectile.GetComponent<Rigidbody>().isKinematic = false;
        Vector3 randomInaccuracy = new(Random.Range(-sO.MaxInaccuracy, sO.MaxInaccuracy), Random.Range(-sO.MaxInaccuracy, sO.MaxInaccuracy), 0);
        Vector3 throwDirection = target.position - (snowSpawn.position + randomInaccuracy);
        projectile.GetComponent<Rigidbody>().AddForce(sO.Force * throwDirection.normalized, ForceMode.Impulse);
        
        Destroy(projectile, 3f);
        cycleThrowCount++;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            hitCount++;
            if (hitCount >= hitsToKill)
            {
                kidActive = false;
                //animator.enabled = false;
                Rigidbody[] rigidbodies = ragdollEnabler.EnableRagdoll();
                //foreach (Rigidbody rb in rigidbodies)
                //{
                //    rb.isKinematic = false;
                //}
                
                Vector3 forceDirection = -(collision.contacts[0].point - transform.position).normalized;
                forceDirection.y = 0;
                foreach (Rigidbody rb in rigidbodies)
                {

                    rb.AddForce(forceDirection * 20f, ForceMode.Impulse);
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
