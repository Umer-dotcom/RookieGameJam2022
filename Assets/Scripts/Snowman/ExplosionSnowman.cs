using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ExplosionSnowman : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    private Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0.1f);
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
            if (nearbyObject.gameObject.CompareTag("Kid"))
            {
                Rigidbody body = nearbyObject.gameObject.GetComponent<Rigidbody>();
                if(body != null)
                {
                    body.AddExplosionForce(force, transform.position, radius);
                    nearbyObject.GetComponent<KidThrowerScript>().KiddoExploded();
                }
                else
                {
                    Rigidbody rb = nearbyObject.gameObject.AddComponent<Rigidbody>();
                    rb.AddExplosionForce(force, transform.position, radius);
                    nearbyObject.GetComponent<KidInfantryScript>().KiddoExploded();
                    Destroy(rb);
                }
            }
        }

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("I am here!");
            currentHits += 1;
            this.transform.localScale += scaleChange;

            if (currentHits >= totalHits)
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