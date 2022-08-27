using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public static LookAt instance;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void LookTowards(Vector3 point)
    {
        transform.LookAt(point);
    }
}
