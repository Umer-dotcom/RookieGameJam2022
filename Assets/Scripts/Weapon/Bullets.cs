using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _particles;
    
    GameObject _hitEffect;
    private void Start()
    {
        _hitEffect = transform.Find("SnowballExplosion").gameObject;
        _hitEffect.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("Colliding with something");
        Debug.Log(collision.gameObject.tag);
        
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Collider>().enabled = false;
        _hitEffect.transform.position = this.transform.position;
        
        if (!_hitEffect.activeInHierarchy) _hitEffect.SetActive(true);

        if (collision.gameObject.CompareTag("Kid"))
        {
            Kid kidScript = collision.gameObject.GetComponent<Kid>();
            //if (kidSScript)
            if (kidScript.GetHitCount() >= kidScript.GetHitsToKill() - 1)
            {
                ParticleSystem ps = _particles[Random.Range(0, 1)];
                ps.gameObject.transform.position = this.transform.position;

                ps.Play();
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        //ParticleSystem ps = _particles[Random.Range(0, 1)];
        //ps.gameObject.transform.position = this.transform.position;
        
        //this.GetComponent<MeshRenderer>().enabled = false;
        //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //this.GetComponent<Collider>().enabled = false;
        ////ps.Play();
        
        //_hitEffect.transform.position = other.transform.position;
        //if (!_hitEffect.activeInHierarchy) _hitEffect.SetActive(true);
    }
}
