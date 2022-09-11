using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidChooserSpawner : MonoBehaviour, IPooledObject
{
    [SerializeField] KidChooserSO kidChooser;
    GameObject chosenKid, instantiatedObject;
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
        Destroy(instantiatedObject);
    }

    void IPooledObject.OnObjectSpawn()
    {
        chosenKid = kidChooser.GetKid();
        instantiatedObject = Instantiate(chosenKid, transform.position, Quaternion.identity);
        instantiatedObject.transform.parent = transform;
        //StartCoroutine(FadeOutEffect());
    }
    IEnumerator FadeOutEffect()
    {
        yield return new WaitForSeconds(kidChooser.duration);
        this.gameObject.SetActive(false);
    }
}
