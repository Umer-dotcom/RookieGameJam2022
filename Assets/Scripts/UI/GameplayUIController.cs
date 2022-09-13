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
        switch (score)
        {
            case 5:
            case 8:
            case 10:
                fade_in();
                break;
        }
    }

    public void scoreDecrement(int score)
    {
        switch (score)
        {
            case 4:
            case 7:
            case 9:
                fade_out();
                break;
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
