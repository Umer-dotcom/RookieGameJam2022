using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public static PlayerHealthBar instance;
    [SerializeField]
    private float healthDec = 0.1f;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateHealthBar()
    {
        GetComponent<Image>().fillAmount -= healthDec;
    }
}
