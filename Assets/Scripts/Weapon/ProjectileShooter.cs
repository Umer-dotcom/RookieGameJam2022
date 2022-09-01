using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
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

    [SerializeField]
    private bool shooting = false;
    [SerializeField]
    private float gunHeat = 0f;
    [SerializeField]
    private float TimeBetweenShots = 0.25f;
    [SerializeField]
    private ParticleSystem MuzzleFlash;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                shooting = true;
            }

            if( touch.phase == TouchPhase.Ended)
            {
                shooting = false;
            }

            if (shooting)
            {
                
                // cool the gun
                if (gunHeat > 0)
                {
                    gunHeat -= Time.deltaTime;
                }

                if (gunHeat <= 0)
                {
                    // heat the gun up so we have to wait a bit before shooting again
                    gunHeat = TimeBetweenShots;

                    // DO THE SHOT HERE
                    MuzzleFlash.Play();
                    GameObject projectile = Instantiate(projectiles[Random.Range(0, projectiles.Length - 1)], spawnPos.position, Quaternion.identity);
                    projectile.transform.parent = projectilesContainer.transform;
                    projectile.GetComponent<Rigidbody>().isKinematic = false;
                    projectile.GetComponent<Rigidbody>().AddForce(shootForce * Time.deltaTime * spawnPos.transform.forward, ForceMode.Impulse);
                    Destroy(projectile, 5f);

                }
                
            }
        }
      

    }

    public void SpawnPosLookAt(Vector3 targetLook)
    {
        spawnPos.transform.LookAt(targetLook);
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(spawnPos.position, spawnPos.transform.forward * 100f, Color.magenta);
    }
}
