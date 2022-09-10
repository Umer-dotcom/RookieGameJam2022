using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    [SerializeField]
    private Debugg config;

    [SerializeField]
    private Slider CamMinLimX;
    [SerializeField]
    private Text CamMinLimXTxt;

    [SerializeField]
    private Slider CamMinLimY;
    [SerializeField]
    private Text CamMinLimYTxt;

    [SerializeField]
    private Slider CamMaxLimX;
    [SerializeField]
    private Text CamMaxLimXTxt;

    [SerializeField]
    private Slider CamMaxLimY;
    [SerializeField]
    private Text CamMaxLimYTxt;

    [SerializeField]
    private Slider CamSens;
    [SerializeField]
    private Text CamSensTxt;

    [SerializeField]
    private Slider CamFire;
    [SerializeField]
    private Text CamFireTxt;

    private void Update()
    {
        CamMinLimXTxt.text = CamMinLimX.value.ToString();
        CamMinLimYTxt.text = CamMinLimY.value.ToString();
        CamMaxLimXTxt.text = CamMaxLimX.value.ToString();
        CamMaxLimYTxt.text = CamMaxLimY.value.ToString();
        CamSensTxt.text = CamSens.value.ToString();
        CamFireTxt.text = CamFire.value.ToString();

        // Updating values
        config.camMinLimitY = CamMinLimY.value;
        config.camMinLimitX = CamMinLimX.value;
        config.camMaxLimitY = CamMaxLimY.value;
        config.camMaxLimitX = CamMaxLimX.value;
        config.camXSensitivity = CamSens.value;
        config.camYSensitivity = CamSens.value;
        config.gunFireRate = CamFire.value;
    }
}
