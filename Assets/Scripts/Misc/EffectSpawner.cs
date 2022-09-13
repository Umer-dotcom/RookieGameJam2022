using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour, IPooledObject
{
    [SerializeField] ParticleEffectSO effectSO;
    GameObject chosenEffect, instantiatedObject;
    private void OnEnable()
    {
        //chosenEffect = effectSO.GetEffect();
        //instantiatedObject = Instantiate(chosenEffect, transform.position, Quaternion.identity);
    }
    private void Start()
    {
    }
 
    private void OnDisable()
    {
        if (instantiatedObject != null) Destroy(instantiatedObject);
    }

    void IPooledObject.OnObjectSpawn()
    {
        if (instantiatedObject != null) Destroy(instantiatedObject);
        chosenEffect = effectSO.GetEffect();
        instantiatedObject = Instantiate(chosenEffect, transform.position, Quaternion.identity);
        instantiatedObject.transform.parent = this.transform;
        //StartCoroutine(FadeOutEffect());
    }
    IEnumerator FadeOutEffect()
    {
        yield return new WaitForSeconds(effectSO.duration);
        this.gameObject.SetActive(false);
    }

}
