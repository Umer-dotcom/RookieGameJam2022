using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private float fadeTime = 1f;

    [Header("GameplayUI")]
    [SerializeField]
    private Slider gameplaySlider;
    [SerializeField]
    private int sliderEffectValue = 1;
    [SerializeField]
    private GameplayUIController gameplayUIController;

    [Header("CharacterUnlockUI")]
    [SerializeField]
    private RectTransform charUnlockRectTransform;
    [SerializeField]
    private float unlockSpeed = 20f;
    [SerializeField]
    private float lerpSpeed = 3f;
    [SerializeField]
    private Slider characterUnlockSlider;
    [SerializeField] 
    private bool isCharacterUnlocked;

    [Header("WinScreen")]
    [SerializeField]
    private RectTransform winRectTransform;
    [SerializeField]
    private List<GameObject> stars = new List<GameObject>();

    [Header("LoseScreen")]
    [SerializeField]
    RectTransform loseRectTransform;

    [Header("Main Menu")]
    [SerializeField]
    RectTransform mainMenuRectTransform;

    [Header("Volume Menu")]
    [SerializeField]
    private GameObject volumeScreen;

    [Header("Game Control Menu")]
    [SerializeField]
    private GameObject gameControlScreen;

    [Header("Level Select Menu")]
    [SerializeField]
    private GameObject levelSelectScreen;

    [Header("About Us Menu")]
    [SerializeField]
    private GameObject aboutUsScreen;

    [Header("For Testing Purpose")]
    [SerializeField] public bool inc;
    [SerializeField] private bool dec;
    [SerializeField] private bool unlockChar;
    [SerializeField] public bool winFadeIN;
    [SerializeField] private bool winFadeOUT;
    [SerializeField] public bool loseFadeIN;
    [SerializeField] private bool loseFadeOUT;
    [SerializeField] private bool unlockCharScreenFadeIn;
    [SerializeField] private bool unlockCharScreenFadeOut;

    private void Start()
    {
        WinPanelFadeOut();
        LosePanelFadeOut();
        charUnlockRectTransform.transform.localScale = Vector3.zero;
        FadeOut(mainMenuRectTransform);
    }

    private void Update()
    {
        if(inc)
        {
            inc = false;
            GameplayIncrementSlider();
        }
        if(dec)
        {
            dec = false;
            GameplayDecrementSlider();
        }
        if(unlockChar)
        {
            unlockChar = false;
            UnlockCharacter();
        }
        if(winFadeIN)
        {
            winFadeIN = false;
            WinPanelFadeIn();
        }
        if(winFadeOUT)
        {
            winFadeOUT = false;
            WinPanelFadeOut();
        }
        if (loseFadeIN)
        {
            loseFadeIN = false;
            LosePanelFadeIn();
        }
        if (loseFadeOUT)
        {
            loseFadeOUT = false;
            LosePanelFadeOut();
        }
        if(unlockCharScreenFadeIn)
        {
            unlockCharScreenFadeIn = false;
            CharacterUnlockFadeIn();
        }
        if(unlockCharScreenFadeOut)
        {
            unlockCharScreenFadeOut = false;
            CharacterUnlockFadeOut();
        }

    }

    private void CharacterUnlockFadeIn()
    {
        charUnlockRectTransform.transform.DOScale(1f, fadeTime).SetEase(Ease.OutElastic);
    }

    private void CharacterUnlockFadeOut()
    {
        charUnlockRectTransform.transform.DOScale(Vector3.zero, fadeTime).SetEase(Ease.InElastic);
    }
    
    public void WinPanelFadeIn()
    {
        FadeIn(winRectTransform);
        StartCoroutine("WinningStarsOut");
    }

    private void WinPanelFadeOut()
    {
        FadeOut(winRectTransform);
    }

    public void LosePanelFadeIn()
    {
        FadeIn(loseRectTransform);
    }

    private void LosePanelFadeOut()
    {
        FadeOut(loseRectTransform);
    }

    private void FadeIn(RectTransform rectTransform)
    {
        rectTransform.transform.localPosition = new Vector3(0f, -1500f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
    }

    private void FadeOut(RectTransform rectTransform)
    {
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -2000f), fadeTime, false).SetEase(Ease.OutElastic);
    }

    private void UnlockCharacter()
    {
        if(isCharacterUnlocked == false)
        {
            characterUnlockSlider.value += 20f;
            if(characterUnlockSlider.value >= 100f)
            {
                isCharacterUnlocked = true;
            }
        }
    }

    private void GameplayIncrementSlider()
    {
        Debug.Log("Incrementing slider!");
        gameplaySlider.value = Mathf.Clamp(gameplaySlider.value + sliderEffectValue, gameplaySlider.minValue, gameplaySlider.maxValue);
        gameplayUIController.scoreIncrement((int)gameplaySlider.value);
    }

    private void GameplayDecrementSlider()
    {
        gameplaySlider.value = Mathf.Clamp(gameplaySlider.value - sliderEffectValue, gameplaySlider.minValue, gameplaySlider.maxValue);
        gameplayUIController.scoreDecrement((int)gameplaySlider.value);
    }

    public void GoToMainMenuFromWinScreen()
    {
        WinPanelFadeOut();
        FadeIn(mainMenuRectTransform);
    }

    public void GoToMainMenuFromLoseScreen()
    {
        LosePanelFadeOut();
        FadeIn(mainMenuRectTransform);
    }

    public void GoToVolumeFromMainMenu()
    {
        volumeScreen.SetActive(true);
    }

    public void GoToMainMenuFromVolume()
    {
        volumeScreen.SetActive(false);
    }

    public void GoToGameControlFromMainMenu()
    {
        gameControlScreen.SetActive(true);
    }

    public void GoToMainMenuFromGameControl()
    {
        gameControlScreen.SetActive(false);
    }

    public void GoToLevelSelectFromMainMenu()
    {
        levelSelectScreen.SetActive(true);
    }

    public void GoToMainMenuFromLevelSelect()
    {
        levelSelectScreen.SetActive(false);
    }

    public void GoToAboutUsFromMainMenu()
    {
        aboutUsScreen.SetActive(true);
    }

    public void GoToMainMenuFromAboutUs()
    {
        aboutUsScreen.SetActive(false);
    }

    IEnumerator WinningStarsOut()
    {
        foreach (var s in stars)
        {
            s.transform.localScale = Vector3.zero;
        }

        int counter = 0;
        if (gameplaySlider.value == 10)
            counter = 3;
        else if (gameplaySlider.value >= 8)
            counter = 2;
        else if (gameplaySlider.value >= 5)
            counter = 1;

        Debug.Log("Counter = " + counter.ToString());

        for(int i = 0; i < counter; i++)
        {
            yield return new WaitForSeconds(0.25f);
            stars[i].transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
        }
    }
}
