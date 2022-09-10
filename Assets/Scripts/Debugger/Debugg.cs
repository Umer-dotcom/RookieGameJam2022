using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Debugger", menuName ="DebugConfig")]
public class Debugg : ScriptableObject
{
    public float camMinLimitX;
    public float camMinLimitY;
    public float camMaxLimitX;
    public float camMaxLimitY;

    public float camXSensitivity;
    public float camYSensitivity;

    public float gunFireRate;
}
