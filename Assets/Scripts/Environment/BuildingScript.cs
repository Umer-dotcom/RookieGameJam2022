using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    
    [SerializeField] List<float> blendShapeActiveTime;
    SkinnedMeshRenderer meshRenderer;
    float timePassed;
    int activatedCount = 0;
    private void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }
    private void Update()
    {

        if (blendShapeActiveTime.Count == 0) return;
        if (timePassed >= blendShapeActiveTime[0])
        {
            ActivateBlendShape(activatedCount);
            activatedCount++;
 
            blendShapeActiveTime.RemoveAt(0);
        }
 
        
        timePassed += Time.deltaTime;
    }

    public void ActivateBlendShape(int index)
    {
        int value = 0;
        StopCoroutine(ActivateBlendShapeCoroutine(index, value));
        StartCoroutine(ActivateBlendShapeCoroutine(index, value));

    }

    IEnumerator ActivateBlendShapeCoroutine(int index, int value)
    {
        while (value < 100)
        {
            meshRenderer.SetBlendShapeWeight(index, value);
            value += 1;
            yield return null;

        }
        
    }

}
