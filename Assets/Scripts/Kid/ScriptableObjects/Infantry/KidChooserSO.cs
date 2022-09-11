using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "SO/Enemy/New Enemy Type")]
public class KidChooserSO : DescriptionBaseSO
{
    public List<GameObject> kid;
    public float duration = 2f;
    public GameObject GetKid()
    {
        return kid[Random.Range(0, kid.Count)];
    }
}
