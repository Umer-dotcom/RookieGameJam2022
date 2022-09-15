using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField]
    private Slider volSlider;

    public void UpdateVolume()
    {
        AudioManager.instance.ThemeVolumeAdjustment(volSlider.value);
    }
}
