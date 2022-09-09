using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidInfantrySO : ScriptableObject
{
    [SerializeField] protected int hitsToKill;
    [SerializeField] protected GameObject sleepEffect;
    [SerializeField] protected List<GameObject> intitHitEmojis;
    [SerializeField] protected List<GameObject> midHitEmojis;
    [SerializeField] protected List<GameObject> finalHitEmojis;

    protected int stateTransitionValue;
    private void Awake()
    {
        stateTransitionValue = hitsToKill / 3;
    }
}
