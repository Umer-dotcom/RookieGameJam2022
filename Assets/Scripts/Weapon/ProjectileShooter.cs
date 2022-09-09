using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ProjectileShooter : MonoBehaviour
{
    private enum ShootState {
        COVER,
        SHOOTING

    }
    private ShootState state;

    public static ProjectileShooter instance;

    [SerializeField] GameObject parent;

    [SerializeField] CinemachineVirtualCamera virtualCamera;

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

    ObjectPoolerScript poolerScript;

    private Animator animator;
    Collider mainCollider;
    Vector3 InitCameraPosition;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gunRB = GetComponent<Rigidbody>();
            poolerScript = ObjectPoolerScript.Instance;
            //animator = GetComponent<Animator>();
            gunRB.isKinematic = true;
        }
        else
            Destroy(this);
    }

    private void Start()
    {
        if (virtualCamera != null) InitCameraPosition = virtualCamera.LookAt.position;
        
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

            
        }

        if (!shooting)
        {
            Vector3 velocity = Vector3.zero;
            float smoothTime = 0.3f;
            virtualCamera.LookAt.position = Vector3.SmoothDamp(virtualCamera.LookAt.position, InitCameraPosition, ref velocity, smoothTime);

        }
        Debug.DrawRay(spawnPos.position, spawnPos.forward * 25f, Color.red);


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
        GameObject projectile = poolerScript.SpawnFromPool(OPTag.BULLET, spawnPos.position, Quaternion.identity);/*Instantiate(projectiles[Random.Range(0, projectiles.Length - 1)], spawnPos.position, Quaternion.identity);*/
        projectile.transform.parent = projectilesContainer.transform;
        projectile.GetComponent<Rigidbody>().isKinematic = false;
        projectile.GetComponent<Rigidbody>().AddForce(shootForce * spawnPos.forward, ForceMode.Impulse);

        //Destroy(projectile, 5f);
    }
}
