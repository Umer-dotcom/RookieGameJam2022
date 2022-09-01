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
        
        Debug.Log("Colliding with something");
        
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Collider>().enabled = false;
        _hitEffect.gameObject.transform.position = this.transform.position;
        //if(!_hitEffect.isPlaying) _hitEffect.Play();
        _hitEffect.SetActive(true);

        if (collision.gameObject.CompareTag("Kid"))
        {
            KidScript kidScript = collision.gameObject.GetComponent<KidScript>();
            if (kidScript.GetIcecreamHitCount() == kidScript.icecreamNeeded - 1)
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
        
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Collider>().enabled = false;
        //ps.Play();
        
        _hitEffect.gameObject.transform.position = other.transform.position;
        _hitEffect.SetActive(true);
    }
}
