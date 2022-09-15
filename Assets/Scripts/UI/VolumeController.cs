using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField]
    private Slider volSlider;

    private void Start()
    {
        volSlider.value = AudioManager.instance.GetVolume("Theme");
    }

    private void Update()
    {
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        AudioManager.instance.ThemeVolumeAdjustment(volSlider.value);
    }
}
