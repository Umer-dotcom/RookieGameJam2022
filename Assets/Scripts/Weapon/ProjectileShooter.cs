using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    private enum ShootState {
        COVER,
        SHOOTING

    }
    private ShootState state;

    public static ProjectileShooter instance;

    [SerializeField] GameObject parent;

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

    [SerializeField]
    List<Transform> positions;

    private Animator animator;
    Collider mainCollider;
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
            
            //if (touch.phase == TouchPhase.Began)
            //{
            //    float screenMid = Screen.width / 2;
            //    if (touch.position.x > screenMid)
            //    {
            //        state = ShootState.SHOOTRIGHT;
            //        animator.SetTrigger("leaveCoverRight");
            //        parent.transform.position = positions[0].position;
            //        //parent.transform.position = new(Mathf.Lerp(parent.transform.position.x, positions[0].position.x, 0.3f), parent.transform.position.y ,parent.transform.position.z);
            //    }
            //    else
            //    {
            //        state = ShootState.SHOOTLEFT;
            //        animator.SetTrigger("leaveCoverLeft");
            //        //parent.transform.position = new(Mathf.Lerp(parent.transform.position.x, positions[2].position.x, 0.3f), parent.transform.position.y, parent.transform.position.z);
            //        parent.transform.position = positions[2].position;
            //    }
            //}

            //if( touch.phase == TouchPhase.Ended)
            //{
            //    float velocity = 0.0f;
            //    parent.transform.position = positions[1].position;//new(Mathf.SmoothDamp(parent.transform.position.x, positions[1].position.x, ref velocity, 0.5f), parent.transform.position.y, parent.transform.position.z);
            //    if (state == ShootState.SHOOTRIGHT)
            //    {
            //        animator.SetTrigger("takeCoverRight");
            //        state = ShootState.COVER;
            //    }
            //    else if (state == ShootState.SHOOTLEFT)
            //    {
            //        state = ShootState.COVER;
            //        animator.SetTrigger("takeCoverLeft");
            //    }
                
            //}

            if (touch.phase == TouchPhase.Began)
            {
                shooting = true;
                mainCollider.enabled = true;
                animator.SetTrigger("leaveCover");

            }

            if (touch.phase == TouchPhase.Ended)
            {
                shooting = false;
                mainCollider.enabled = false;
                animator.SetTrigger("takeCover");
                transform.localRotation = Quaternion.identity;
            }
            animator.SetBool("shooting", shooting);

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
    public void StopGun()
    {
        gunActive = false;
    }
    public void StartGun()
    {
        gunActive = true;
    }
    public void SetAnimator(Animator animator)
    {
        this.animator = animator;
        animator.SetFloat("speedMultiplier", shootSpeedMultiplier);
    }
    public void SetCollider(Collider collider)
    {
        this.mainCollider = collider;
        collider.enabled = false;
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
