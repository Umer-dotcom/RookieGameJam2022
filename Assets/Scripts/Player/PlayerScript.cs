using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int hitsToKill;
    [SerializeField] Transform RagdollRoot;

    public static event Action playerDeathEvent = delegate { };
    //public static event Action playerHitEvent = delegate { };

    Animator animator;
    private int hitCount = 0;
    private bool playerActive = true;
    protected ProjectileShooter gun;
    CapsuleCollider mainCol;
    Rigidbody[] rigidbodies;
    Joint[] joints;
    bool StartRagdoll = false;
    //RagdollEnabler ragdollEnabler;
    CinemachineShake cinemachineShake;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        mainCol = GetComponent<CapsuleCollider>();
        gun = GetComponentInChildren<ProjectileShooter>();
        animator = GetComponent<Animator>();
        gun.SetAnimator(animator);
        gun.SetCollider(mainCol);
        cinemachineShake = CinemachineShake.INSTANCE;

        rigidbodies = RagdollRoot.GetComponentsInChildren<Rigidbody>();
        joints = RagdollRoot.GetComponentsInChildren<CharacterJoint>();

        if (StartRagdoll)
        {
            EnableRagdoll();
        }
        else
        {
            EnableAnimator();
        }

        mainCol.enabled = true;
    }

    public void EnableRagdoll()
    {
        animator.enabled = false;

        foreach (CharacterJoint joint in joints)
        {
            joint.enableCollision = true;
        }
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.detectCollisions = true;
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
        }
    }

    public void DisableAllRigidbodies()
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.detectCollisions = false;
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }
    }

    public void EnableAnimator()
    {
        animator.enabled = true;
        foreach (CharacterJoint joint in joints)
        {
            joint.enableCollision = false;
        }
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.detectCollisions = false;
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }
    }

    protected void OnTriggerEnter(Collider other)    
    {
        //Debug.Log("Player hit.");
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Player hit.");
            CinemachineShake.INSTANCE.ShakeCamera(5f, 0.1f);
            hitCount++;
            if (hitCount >= hitsToKill)
            {
                playerActive = false;
                playerDeathEvent?.Invoke();

                //ProjectileShooter.instance.GunDrop();
                EnableRagdoll();
                
                Vector3 forceDirection = -(transform.forward).normalized;
                forceDirection.y = 0;
                foreach (Rigidbody rb in rigidbodies)
                {
                    rb.AddForce(forceDirection * 10f, ForceMode.Impulse);
                }
                Time.timeScale = 0.5f;
                Collider mainCollider = GetComponent<Collider>();
                
                mainCollider.enabled = false;
                StartCoroutine(TurnKidsOffDelay(3f));
            }
        }
    }

    protected IEnumerator TurnKidsOffDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1f;
        DisableAllRigidbodies();
    }
    public void GetPlayerToShoot()
    {
        //MakeColliderVisible();
        gun.Shoot();
    }
    public void MakeColliderVisible()
    {
        //Bounds mainColBounds = mainCol.bounds;
        mainCol.enabled = true;
    }
}
