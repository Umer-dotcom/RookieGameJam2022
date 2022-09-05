using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowThrowerScript : MonoBehaviour
{
    [SerializeField] GameObject snowPrefab;
    [SerializeField] float throwCooldown;
    [SerializeField] Transform target;
    [SerializeField] GameObject projectilesContainer;
    [SerializeField] int shootForce;

    float timeBetweenLastThrow = 0;

    

    void Update()
    {
        timeBetweenLastThrow += Time.deltaTime;
        
        if (timeBetweenLastThrow >= throwCooldown)
        {
            GameObject projectile = Instantiate(snowPrefab, transform);

            timeBetweenLastThrow = 0;
            projectile.transform.parent = projectilesContainer.transform;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            Vector3 throwDirection = target.position - transform.position;
            projectile.GetComponent<Rigidbody>().AddForce(shootForce * Time.deltaTime * throwDirection, ForceMode.Impulse);
            Destroy(projectile, 3f);
        }
    }
}
