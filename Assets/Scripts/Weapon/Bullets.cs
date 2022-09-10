using System;
using UnityEngine;

public class Bullets : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField]
    private ParticleSystem[] _particles;
    
    GameObject _hitEffect;
    private void Start()
    {
        _hitEffect = transform.Find("SnowballExplosion").gameObject;
        _hitEffect.SetActive(false);
=======
    private const string kidTag = "Kid";
    private const string headTag = "Head";
    ObjectPoolerScript poolerScript;
    private MeshRenderer meshRenderer;
    private Collider mainColider;
    private Rigidbody rb;

    public static event Action hitEvent = delegate { };
    public static event Action headShotEvent = delegate { };


    private void Awake()
    {
        poolerScript = ObjectPoolerScript.Instance;
        meshRenderer = GetComponent<MeshRenderer>();
        mainColider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        meshRenderer.enabled = true;
        rb.velocity = Vector3.zero;

        mainColider.enabled = true;

    }
    private void OnDisable()
    {

        meshRenderer.enabled = false;
        rb.velocity = Vector3.zero;
        mainColider.enabled = false;

>>>>>>> Stashed changes
    }
    private void OnCollisionEnter(Collision collision)
    {

<<<<<<< Updated upstream
        //Debug.Log("Colliding with something");
        Debug.Log(collision.gameObject.tag);
        
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Collider>().enabled = false;
        _hitEffect.transform.position = this.transform.position;
        
        if (!_hitEffect.activeInHierarchy) _hitEffect.SetActive(true);

        if (collision.gameObject.CompareTag("Kid"))
=======
        meshRenderer.enabled = false;
        rb.velocity = Vector3.zero;
        mainColider.enabled = false;

        poolerScript.SpawnFromPool(OPTag.SNOWEFFECT, transform.position, Quaternion.identity);

        if (collision.gameObject.CompareTag(kidTag))
>>>>>>> Stashed changes
        {
            hitEvent?.Invoke();
            Kid kidScript = collision.gameObject.GetComponent<Kid>();
            //if (kidSScript)
            if (kidScript.GetHitCount() >= kidScript.GetHitsToKill() - 1)
            {
<<<<<<< Updated upstream
                ParticleSystem ps = _particles[Random.Range(0, 1)];
                ps.gameObject.transform.position = this.transform.position;

                ps.Play();
            }
=======
                poolerScript.SpawnFromPool(OPTag.HIT, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                
            }
        } else if (collision.gameObject.CompareTag(headTag))
        {
            headShotEvent?.Invoke();
            poolerScript.SpawnFromPool(OPTag.CRIT, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
>>>>>>> Stashed changes
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
