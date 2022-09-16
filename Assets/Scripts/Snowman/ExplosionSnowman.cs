using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ExplosionSnowman : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    private float radius = 5f;
    [SerializeField]
    private float force = 700f;
    [SerializeField]
    private int totalHits = 5;

    [Header("For Testing Purpose")]
    [SerializeField]
    private bool boom = false;
    [SerializeField]
    private bool hasExploded = false;
    [SerializeField]
    private int currentHits = 0;

    void Update()
    {
        if (boom && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void Explode()
    {
        GameObject obj = Instantiate(explosionEffect, transform.position, transform.rotation);
        obj.SetActive(true);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            // Add force
            if(nearbyObject.gameObject.CompareTag("Kid"))
            {
                Rigidbody rb = nearbyObject.gameObject.AddComponent<Rigidbody>();
                rb.AddExplosionForce(force, transform.position, radius);
                nearbyObject.GetComponent<KidInfantryScript>().KiddoExploded();
                Destroy(rb);
            }
            //Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            //if (rb != null)
            //{
            //    if(rb.gameObject.CompareTag("Kid"))
            //    {
            //        Debug.Log(nearbyObject.gameObject.name);
            //        rb.AddExplosionForce(force, transform.position, radius);
            //    }
            //}
            // Damage
        }

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            currentHits += 1;
            this.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

            if(currentHits >= totalHits)
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
