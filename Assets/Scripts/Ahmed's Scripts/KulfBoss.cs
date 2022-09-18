using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulfBoss : MonoBehaviour
{
    public float scalespeed = 1f;
    public Vector3 scalering = new Vector3(0.1f,0.1f,0.1f);
    public float scalelimit = 3f;




    // Code that is in action
    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    private Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0.1f);


    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x !> scalelimit)
        {
            if(transform.localScale.x < scalelimit)
            {
                Invoke("scaler", scalespeed);
            }
        }
        if(transform.localScale.x < .5f)
        {
            Explode();
        }
    }

    void scaler()
    {
        transform.localScale += scalering;
    }

    private void Explode()
    {
        GameObject obj = Instantiate(explosionEffect, transform.position, transform.rotation);
        obj.SetActive(true);

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GotHit();
        }
    }

    private void GotHit()
    {
        transform.localScale = transform.localScale - scaleChange;

    }
}
