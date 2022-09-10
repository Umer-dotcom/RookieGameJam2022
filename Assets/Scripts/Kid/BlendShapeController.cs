using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeController : MonoBehaviour
{

    bool mouthOpen = false;
    int mouthLevel = 0;
    int fatLevel = 0;
    bool stomachFilled = false;
    int stomachLevel = 0;
    SkinnedMeshRenderer skinnedMeshRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        
    }
    public void OpenMouth()
    {
        if (stomachFilled) return;
        mouthOpen = true;
        mouthLevel = 0;
        StopCoroutine(CloseMouthCoroutine());
        StartCoroutine(OpenMouthCoroutine());

        StopCoroutine(MouthCloseDelayFunc());
        StartCoroutine(MouthCloseDelayFunc());
    }
    public void CloseMouth()
    {
        if (stomachFilled) return;
        mouthOpen = false;
        mouthLevel = 100;
        StopCoroutine(OpenMouthCoroutine());
        StartCoroutine(CloseMouthCoroutine());
    }

    IEnumerator OpenMouthCoroutine()
    {
        
        while (mouthLevel <= 100)
        {
            
            skinnedMeshRenderer.SetBlendShapeWeight(0, mouthLevel);
            mouthLevel += 10;
            yield return null;
        }
    }
    IEnumerator CloseMouthCoroutine()
    {
        
        while (mouthLevel >= 0)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(0, mouthLevel);
            mouthLevel -= 10;
            yield return null;
        }
    }
    public void MakeFat(int iceCreamNeeded, int currentIceCreamCount)
    {
        int blendIntervals = 300 / iceCreamNeeded;
        int blendValue = currentIceCreamCount * blendIntervals;
        Debug.Log(blendValue);
        if (blendValue > 300) return;
        StopCoroutine(MakeFatCoroutine(blendValue));
        StartCoroutine(MakeFatCoroutine(blendValue));
    }

    IEnumerator MakeFatCoroutine(int blendValue)
    {
        while (fatLevel <= blendValue)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(1, fatLevel);
            fatLevel += 10;
            yield return null;
        }
    }
    public void StomachFilled()
    {
        stomachFilled = true;
        if(mouthOpen) CloseMouth();
        StopCoroutine(StomachFilledCoroutine());
        StartCoroutine(StomachFilledCoroutine());
    }
    IEnumerator StomachFilledCoroutine()
    {
        while (stomachLevel <= 100)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(2, stomachLevel);
            stomachLevel += 10;
            yield return null;
        }
    }

    public bool IsMouthOpen()
    {
        return mouthOpen;
    }
    IEnumerator MouthCloseDelayFunc()
    {
        yield return new WaitForSeconds(0.5f);
        CloseMouth();
    }
}
