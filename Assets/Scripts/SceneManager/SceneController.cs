using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [SerializeField]
    private Scene[] scenes;
    [SerializeField]
    private int resumeFromLevel = 0;
    [Header("Testing")]
    [SerializeField]
    private bool playerPrefFound = false;

    private int totalScenesCount = 0;

    private void Awake()
    {
        // Creating instance
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // Storing all scenes in an array
        totalScenesCount = SceneManager.sceneCountInBuildSettings - 1;
        scenes = new Scene[totalScenesCount];
        
        for(int i = 0; i < totalScenesCount; i++)
        {
            scenes[i] = SceneManager.GetSceneByBuildIndex(i + 1);
        }

        // Check if player data exist
        if(PlayerPrefs.HasKey("ResumeGame"))
        {
            resumeFromLevel = PlayerPrefs.GetInt("ResumeGame");
            playerPrefFound = true;
        }
        else
        {
            resumeFromLevel = 1;
        }
    }

    public void StartTheGame()
    {
        if(resumeFromLevel > totalScenesCount)
        {
            resumeFromLevel = 1;
        }

        SceneManager.LoadSceneAsync(resumeFromLevel);   // Resume game from level where player left
        SceneManager.UnloadSceneAsync(0);   // Unload Start Screen
    }

    public void SaveUnlockedLevel()
    {
        resumeFromLevel = SceneManager.GetActiveScene().buildIndex + 1;
        resumeFromLevel = Mathf.Clamp(resumeFromLevel, 1, totalScenesCount);
        PlayerPrefs.SetInt("ResumeGame", resumeFromLevel);
        PlayerPrefs.SetInt("UnlockedLvls", resumeFromLevel);
    }

    public void RestartScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);   // Resume game from level where player left
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);   // Unload Start Screen
    }
}
