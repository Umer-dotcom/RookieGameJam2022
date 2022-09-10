using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageScript : MonoBehaviour
{
    [SerializeField] int hitsRequiredToRage = 30;
    PlayerScript player;
    Animator animator;
    int r_hitCounter = 0;
    public bool rageAvailable = true;

    public const string rageTag = "rage";
    private void Awake()
    {
        player = GetComponent<PlayerScript>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        Bullets.hitEvent += Hit;
        Bullets.headShotEvent += HeadShot;
    }
    private void Hit()
    {
        r_hitCounter += 2;
        if (r_hitCounter >= hitsRequiredToRage)
        {
            rageAvailable = true;
        }
    }
    private void HeadShot()
    {
        r_hitCounter += 5;
        if (r_hitCounter >= hitsRequiredToRage)
        {
            rageAvailable = true;
        }
    }
    public bool IsRageAvailable()
    {
        return rageAvailable;
    }
    public void GoIntoRageMode()
    {
        animator.SetTrigger(rageTag);
    }
}
