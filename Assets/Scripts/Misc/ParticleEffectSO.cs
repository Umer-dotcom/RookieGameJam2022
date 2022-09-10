using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "SO/Particle Effect/New Effect Type")]
public class ParticleEffectSO : DescriptionBaseSO
{
    public List<GameObject> emojis;
    public float duration = 2f;
    public GameObject GetEffect()
    {
        Debug.Log(emojis.Count);
        return emojis[Random.Range(0, emojis.Count)];
    }
}
