using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
    
    [SerializeField] protected int hitsToKill;
    //[SerializeField] protected GameObject sleepEffect;

    protected ObjectPoolerScript poolerScript;
    

    protected int stateTransitionValue;
    
    protected static int kidCount = 0;

    protected int hitCount = 0;
    private int kidID;
    protected bool kidActive = true;

    protected RagdollEnabler ragdollEnabler;
    private void Awake()
    {
        ragdollEnabler = GetComponent<RagdollEnabler>();
        
    }

    public virtual void Start()
    {
        stateTransitionValue = hitsToKill / 3;
        kidID = kidCount;
        kidCount++;
        Collider mainCol = GetComponent<Collider>();
        mainCol.enabled = true;
    }

    public int GetHitsToKill()
    {
        return hitsToKill;
    }
    public int GetHitCount()
    {
        return hitCount;
    }

    public int GetTotalKidCount()
    {
        return kidCount;
    }
    public int GetKidID()
    {
        return kidID;
    }
    public bool IsKidActive()
    {
        return kidActive;
    }


    protected virtual IEnumerator TurnKidsOffDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("parent coroutine running");
        
    }


}
