using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{

    private enum ShootState {
        COVER,
        SHOOTLEFT,
        SHOOTRIGHT,

    }
    private ShootState state;

    public static ProjectileShooter instance;

    [SerializeField]
    private GameObject[] projectiles;
    [SerializeField]
    private GameObject projectilesContainer;
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    private float shootForce;

    private Touch touch;

    private Rigidbody gunRB;
    private bool gunActive = true;

    private bool shooting = false;
    [SerializeField]
    private float gunHeat = 0f;
    [SerializeField]
    private float shootSpeedMultiplier;
    [SerializeField]
    private ParticleSystem MuzzleFlash;

    private Animator animator;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gunRB = GetComponent<Rigidbody>();
            //animator = GetComponent<Animator>();
            gunRB.isKinematic = true;
        }
        else
            Destroy(this);
    }
    private void OnEnable()
    {
        PlayerScript.playerDeathEvent += GunDrop;
    }
    private void OnDisable()
    {
        PlayerScript.playerDeathEvent -= GunDrop;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunActive && Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                float screenMid = Screen.width / 2;
                if (touch.position.x > screenMid)
                {
                    state = ShootState.SHOOTRIGHT;
                    animator.SetTrigger("leaveCoverRight");
                } else
                {
                    state = ShootState.SHOOTLEFT;
                    animator.SetTrigger("leaveCoverLeft");
                }
                //ducking = false;
                
            }

            if( touch.phase == TouchPhase.Ended)
            {
                if (state == ShootState.SHOOTRIGHT)
                {
                    animator.SetTrigger("takeCoverRight");
                    state = ShootState.COVER;
                }
                else if (state == ShootState.SHOOTLEFT)
                {
                    state = ShootState.COVER;
                    animator.SetTrigger("takeCoverLeft");
                }
            }

            //if (shooting)
            //{

                
            //    // cool the gun
            //    if (gunHeat > 0)
            //    {
            //        gunHeat -= Time.deltaTime;
            //    }

            //    if (gunHeat <= 0)
            //    {
            //        // heat the gun up so we have to wait a bit before shooting again
            //        gunHeat = TimeBetweenShots;

            //        // DO THE SHOT HERE
            //        //MuzzleFlash.Play();
            //        //GameObject projectile = Instantiate(projectiles[Random.Range(0, projectiles.Length - 1)], spawnPos.position, Quaternion.identity);
            //        //projectile.transform.parent = projectilesContainer.transform;
            //        //projectile.GetComponent<Rigidbody>().isKinematic = false;
            //        //projectile.GetComponent<Rigidbody>().AddForce(shootForce * spawnPos.forward, ForceMode.Impulse);
                    
            //        //Destroy(projectile, 5f);

            //    }
                
            //}
        }
        Debug.DrawRay(spawnPos.position, spawnPos.forward * 25f, Color.red);


    }
    public void SetAnimator(Animator animator)
    {
        this.animator = animator;
        animator.SetFloat("shootSpeed", shootSpeedMultiplier);
    }
    public void GunDrop()
    {
        gunActive = false;
        gunRB.isKinematic = false;
    }

    public void SpawnPosLookAt(Vector3 targetLook)
    {
        spawnPos.transform.LookAt(targetLook);
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(spawnPos.position, spawnPos.transform.forward * 100f, Color.magenta);
    }
    public void Shoot()
    {
        MuzzleFlash.Play();
        GameObject projectile = Instantiate(projectiles[Random.Range(0, projectiles.Length - 1)], spawnPos.position, Quaternion.identity);
        projectile.transform.parent = projectilesContainer.transform;
        projectile.GetComponent<Rigidbody>().isKinematic = false;
        projectile.GetComponent<Rigidbody>().AddForce(shootForce * spawnPos.forward, ForceMode.Impulse);

        Destroy(projectile, 5f);
    }
}
