using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int hitsToKill;

    public static event Action playerDeathEvent = delegate { };
    //public static event Action playerHitEvent = delegate { };

    private int hitCount = 0;
    private bool playerActive = true;
    protected Rigidbody[] rigidbodies;
    protected Collider[] colliders;
    private ProjectileShooter gun;
    Collider mainCol;
    //CinemachineShake cinemachineShake;
    // Start is called before the first frame update
    void Start()
    {
        mainCol = GetComponent<Collider>();
        gun = GetComponentInChildren<ProjectileShooter>();
        gun.SetAnimator(GetComponent<Animator>());
        gun.SetCollider(mainCol);
        //cinemachineShake = CinemachineShake.INSTANCE;
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
        
        //mainCol.enabled = true;
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
                //playerDeathEvent();
                ProjectileShooter.instance.GunDrop();
                foreach (Rigidbody rb in rigidbodies)
                {
                    rb.isKinematic = false;
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
