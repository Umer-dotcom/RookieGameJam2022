using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _particles;
    private void OnCollisionEnter(Collision collision)
    {
        ParticleSystem ps = _particles[Random.Range(0, 1)];
        ps.gameObject.transform.position = this.transform.position;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Collider>().enabled = false;
        ps.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem ps = _particles[Random.Range(0, 1)];
        ps.gameObject.transform.position = this.transform.position;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Collider>().enabled = false;
        ps.Play();
    }
}
