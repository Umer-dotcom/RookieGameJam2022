using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private int healthDec = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            slider.value -= 1;
            if (slider.value <= 0)
            {
                slider.gameObject.SetActive(false);
            }
        }
    }
}