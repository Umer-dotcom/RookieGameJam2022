using System.Collections;
using System;
using UnityEngine;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(Animator))]
public class KidThrowerScript : Kid
{
    [Header("Throwing")]
    //[SerializeField] private GameObject snowPrefab;
    [SerializeField] private Transform snowSpawn;
    [SerializeField] private Transform target;
    [SerializeField] private KidThrowerSO sO;

    public static event Action ThrowerKilledEvent = delegate { };


    KidState kidState = KidState.NONE;
    //RagdollEnabler ragdollEnabler;

    int ragdollForce = 20;
    float timeInHidePhase = 0;
    int cycleThrowCount = 0;
    bool hiding = false;
    Animator animator;

    int state1;
    int state2;
    int state3;
    private void Awake()
    {
        
    }

    protected new void Start()
    {
        base.Start();
        poolerScript = ObjectPoolerScript.Instance;
        ragdollEnabler = GetComponent<RagdollEnabler>();
        animator = GetComponent<Animator>();
        animator.SetFloat("speed", sO.SpeedMultiplier);

        state1 = stateTransitionValue;
        state2 = stateTransitionValue * 2;
        state3 = hitsToKill - 1;
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
        GameObject projectile = poolerScript.SpawnFromPool(OPTag.ENEMYBULLET, snowSpawn.position, Quaternion.identity);
        
        projectile.GetComponent<Rigidbody>().isKinematic = false;
        Vector3 randomInaccuracy = new(UnityEngine.Random.Range(-sO.MaxInaccuracy, sO.MaxInaccuracy), UnityEngine.Random.Range(-sO.MaxInaccuracy, sO.MaxInaccuracy), 0);
        Vector3 throwDirection = target.position - (snowSpawn.position + randomInaccuracy);
        projectile.GetComponent<Rigidbody>().AddForce(sO.Force * throwDirection.normalized, ForceMode.Impulse);
        
        //Destroy(projectile, 3f);
        cycleThrowCount++;
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
                        GetAggressive(0.5f);

                        poolerScript.SpawnFromPool(OPTag.NAUGHTYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        break;
                    case KidState.NAUGHTY:
                        GetAggressive(1f);

                        poolerScript.SpawnFromPool(OPTag.NAUGHTYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        break;
                    case KidState.ANGRY:
                        GetAggressive(1.5f);
                        poolerScript.SpawnFromPool(OPTag.ANGRYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        break;

                }
            }

            if (hitCount >= hitsToKill)
            {
                ThrowerKilledEvent?.Invoke();
                kidActive = false;
                //animator.enabled = false;
                Rigidbody[] rigidbodies = ragdollEnabler.EnableRagdoll();

                
                Vector3 forceDirection = -(collision.contacts[0].point - transform.position).normalized;
                forceDirection.y = 0;
                foreach (Rigidbody rb in rigidbodies)
                {

                    rb.AddForce(forceDirection * 20f, ForceMode.Impulse);
                    
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


    private void GetAggressive(float value)
    {
        animator.SetFloat("speed", sO.SpeedMultiplier + value);
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
                    GetAggressive(0.5f);

                    poolerScript.SpawnFromPool(OPTag.NAUGHTYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                    break;
                case KidState.NAUGHTY:
                    GetAggressive(1);

                    poolerScript.SpawnFromPool(OPTag.NAUGHTYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                    break;
                case KidState.ANGRY:
                    GetAggressive(1.5f);
                    poolerScript.SpawnFromPool(OPTag.ANGRYEMOJI, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                    break;

            }
        }




        if (hitCount >= hitsToKill)
        {
            ThrowerKilledEvent?.Invoke();
            ragdollForce = 50;
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

    public void KiddoExploded()
    {
        hitCount = hitsToKill;

        if (hitCount >= hitsToKill)
        {
            ThrowerKilledEvent?.Invoke();

            kidActive = false;
            //animator.enabled = false;
            Rigidbody[] rigidbodies = ragdollEnabler.EnableRagdoll();


            Vector3 forceDirection = -transform.position.normalized;
            forceDirection.y = 0;
            foreach (Rigidbody rb in rigidbodies)
            {

                rb.AddForce(forceDirection * 20f, ForceMode.Impulse);

            }


            Collider mainCollider = GetComponent<Collider>();

            mainCollider.enabled = false;
            StartCoroutine(TurnKidsOffDelay(3f));
        }
    }
}