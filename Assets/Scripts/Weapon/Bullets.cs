using System;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem[] _particles;
    
    //[SerializeField]
    //private ParticleSystem[] _particles;
    private const string kidTag = "Kid";
    private const string headTag = "Head";

    GameObject _hitEffect;
    ObjectPoolerScript poolerScript;
    private MeshRenderer meshRenderer;
    private Collider mainColider;
    private Rigidbody rb;
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
        //_hitEffect.SetActive()
    }
    private void OnDisable()
    {
        //this.GetComponent<MeshRenderer>().enabled = false;
        //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //this.GetComponent<Collider>().enabled = false;

        meshRenderer.enabled = false;
        rb.velocity = Vector3.zero;
        mainColider.enabled = false;
        //_hitEffect.SetActive(false);

    }
    private void Start()
    {

        _hitEffect = transform.Find("SnowballExplosion").gameObject;
        _hitEffect.SetActive(false);

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


        
        //_hitEffect = transform.Find("SnowballExplosion").gameObject;
        //_hitEffect.SetActive(false);

    }
    private void OnCollisionEnter(Collision collision)
    {


        //Debug.Log("Colliding with something");
        Debug.Log(collision.gameObject.tag);


        if (collision.gameObject.CompareTag("Kid"))


        //this.GetComponent<MeshRenderer>().enabled = false;
        //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //this.GetComponent<Collider>().enabled = false;
        // _hitEffect.transform.position = this.transform.position;

        //if (!_hitEffect.activeInHierarchy) _hitEffect.SetActive(true);


        meshRenderer.enabled = false;
        rb.velocity = Vector3.zero;
        mainColider.enabled = false;

        poolerScript.SpawnFromPool(OPTag.SNOWEFFECT, transform.position, Quaternion.identity);

        if (collision.gameObject.CompareTag(kidTag))
       {
            hitEvent?.Invoke();
            Kid kidScript = collision.gameObject.GetComponent<Kid>();
            //if (kidSScript)
            if (kidScript.GetHitCount() >= kidScript.GetHitsToKill() - 1)
            {

                ParticleSystem ps = _particles[Random.Range(0, 1)];
                ps.gameObject.transform.position = this.transform.position;

                poolerScript.SpawnFromPool(OPTag.HIT, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                //ParticleSystem ps = _particles[Random.Range(0, 1)];
                //ps.gameObject.transform.position = this.transform.position;


                //ps.Play();
            }

                poolerScript.SpawnFromPool(OPTag.HIT, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                
            }
        } else if (collision.gameObject.CompareTag(headTag))
        {
            headShotEvent?.Invoke();
            poolerScript.SpawnFromPool(OPTag.CRIT, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);

        } else if (collision.gameObject.CompareTag(headTag))
        {
            poolerScript.SpawnFromPool(OPTag.CRIT, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);

        }

        this.gameObject.SetActive(false);
    }
}
