using System;
using System.Collections.Generic;
using UnityEngine;
using ComboSystem.Scripts;
public class Bullets : MonoBehaviour
{
    
    private const string kidTag = "Kid";
    private const string headTag = "Head";
    ObjectPoolerScript poolerScript;
    private MeshRenderer meshRenderer;
    private Collider mainColider;
    private Rigidbody rb;

    public static event Action<int> HitEvent = delegate { };

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

        meshRenderer.enabled = false;
        rb.velocity = Vector3.zero;
        mainColider.enabled = false;
        //_hitEffect.SetActive(false);
        

    }
    private void Start()
    {
        
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {

        meshRenderer.enabled = false;
        rb.velocity = Vector3.zero;
        mainColider.enabled = false;

        poolerScript.SpawnFromPool(OPTag.SNOWEFFECT, transform.position, Quaternion.identity);

        if (collision.gameObject.CompareTag(kidTag))
        {
            HitEvent?.Invoke(1);
            Kid kidScript = collision.gameObject.GetComponent<Kid>();
            //if (kidSScript)
            if (kidScript.GetHitCount() >= kidScript.GetHitsToKill() - 1)
            {
                poolerScript.SpawnFromPool(OPTag.HIT, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                
            }
        } else if (collision.gameObject.CompareTag(headTag))
        {
            HitEvent?.Invoke(3);
            poolerScript.SpawnFromPool(OPTag.CRIT, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        }

        this.gameObject.SetActive(false);
    }
}
