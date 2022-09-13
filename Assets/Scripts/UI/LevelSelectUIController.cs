using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectUIController : MonoBehaviour
{
    [SerializeField]
    private float fadeTime = 1f;
    [SerializeField]
    private List<GameObject> levels = new List<GameObject>();

    [Header("For Testing")]
    [SerializeField] private bool unlock;
    [SerializeField] private int unlockedLevelsCount = 0;

    private int count = 2;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("UnlockedLvls"))
        {
            unlockedLevelsCount = PlayerPrefs.GetInt("UnlockedLvls");
        }

        for(int i = 1; i < levels.Count; i++)
        {
            levels[i].transform.GetChild(0).gameObject.SetActive(true);
            levels[i].transform.GetChild(0).transform.DOScale(Vector3.zero, fadeTime).SetEase(Ease.InElastic);
        }

        for (int i = 2; i <= unlockedLevelsCount; i++)
        {
            UnlockLevel(i);
        }
    }

    private void Update()
    {
        if (unlock)
        {
            unlock = false;
            UnlockLevel(count);
            count++;
        }
    }

    public void UnlockLevel(int lvl_no)
    {
        if(lvl_no >= 1 && lvl_no <= levels.Count)
        {
            PlayerPrefs.SetInt("UnlockedLvls", count);
            StartCoroutine(Unlocker(lvl_no));
        }
    }

    IEnumerator Unlocker(int lvl_no)
    {
        // Fade Out the lock
        levels[lvl_no - 1].transform.GetChild(1).transform.DOScale(Vector3.zero, fadeTime).SetEase(Ease.InElastic);
        yield return new WaitForSeconds(1f);
        // Fade In the number
        levels[lvl_no - 1].transform.GetChild(0).transform.DOScale(1f, fadeTime).SetEase(Ease.OutElastic);
    }
}
