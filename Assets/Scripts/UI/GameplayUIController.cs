using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField]
    private float fadeTime = 1f;
    [SerializeField]
    private TextMeshProUGUI level_no;
    [SerializeField]
    private List<GameObject> stars = new List<GameObject>();

    [Header("Star PopUp Values")]
    [SerializeField]
    public int star1_val;
    [SerializeField]
    public int star2_val;
    [SerializeField]
    public int star3_val;

    private int star_counter = 0;

    private void Start()
    {
        foreach (var t in stars)
        {
            t.transform.localScale = Vector3.zero;
        }
    }

    public void scoreIncrement(int score)
    {
        if(star1_val == score)
        {
            fade_in();
        }
        if (star2_val == score)
        {
            fade_in();
        }
        if (star3_val == score)
        {
            fade_in();
        }
    }

    public void scoreDecrement(int score)
    {
        if (star1_val - 1 == score)
        {
            fade_out();
        }
        if (star2_val - 1 == score)
        {
            fade_out();
        }
        if (star3_val - 1 == score)
        {
            fade_out();
        }
    }

    private void fade_in()
    {
        if(star_counter < stars.Count)
        {
            stars[star_counter].transform.DOScale(0.3f, fadeTime).SetEase(Ease.OutElastic);
            star_counter++;
        }
    }

    private void fade_out()
    {
        if (star_counter >= 0)
        {
            star_counter--;
            stars[star_counter].transform.DOScale(Vector3.zero, fadeTime).SetEase(Ease.InElastic);
        }
    }
}
