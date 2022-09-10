using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Thrower", menuName = "SO/Kid/New Thrower Type")]
public class KidThrowerSO : DescriptionBaseSO
{
    [Header("Throwing")]
    [SerializeField] private int force;

    [SerializeField] private float speedMultiplier;

    [SerializeField] private int throwsPerCycle;

    [SerializeField] private float maxInaccuracy;

    [Header("Hiding")]
    [SerializeField] private float hidePhaseDuration;

    public int Force { get => force; }
    public float SpeedMultiplier { get => speedMultiplier; }

    public int ThrowsPerCycle { get => throwsPerCycle; }

    public float MaxInaccuracy { get => maxInaccuracy; }
    public float HidePhaseDuration { get => hidePhaseDuration; }

}
