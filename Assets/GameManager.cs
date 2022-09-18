using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dancingHenry;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private UIManager _UIManager;
    [SerializeField]
    private KidSpawnerScript kidSpawnerScript;
    [SerializeField]
    private GameObject[] disableThese;
    [SerializeField]
    private List<GameObject> _kidsList;
    [SerializeField]
    private int hitsToKill = 3;
    [SerializeField]
    private int totalKids = 0;
    [SerializeField]
    private int shooterKids = 0;

    [Header("For Testing Purpose")]
    [SerializeField]
    private bool letsEnd = false;
    [SerializeField]
    private bool allDead = false;
    [SerializeField]
    private bool isPlayerDead = false;
    [SerializeField] 
    int removedKiddos = 0;

    private void Awake()
    {
        AudioManager.instance.Stop("Celebration");
        AudioManager.instance.Play("Theme");
        if (dancingHenry)
        {
            dancingHenry.transform.position = _player.transform.position;
            dancingHenry.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            dancingHenry.SetActive(false);
        }
        else
            Debug.LogError("Dancing Henry not found!");
    }

    private void Start()
    {
        letsEnd = true;

        GameObject[] spawnSystems = GameObject.FindGameObjectsWithTag("spawnsystem");
        foreach (GameObject spawnSystem in spawnSystems)
        {
            totalKids += spawnSystem.GetComponent<KidSpawnerScript>().maxKidCount;
        }

        totalKids += shooterKids;
    }

    private void Update()
    {
        if (_player.GetComponent<Collider>().enabled == false && isPlayerDead == false)
        {
            isPlayerDead = true;
            YouLose();
        }

        if(letsEnd)
        {
            for(int i = 0; i < _kidsList.Count; i++)
            {
                if (_kidsList[i].GetComponent<Collider>().enabled == false)
                {
                    removedKiddos++;
                    _UIManager.inc = true;
                    _kidsList.Remove(_kidsList[i]);
                }
            }
            
            if(removedKiddos == totalKids)
            {
                StopTheGameplay(); // <--
                HenryDoYourDance(); // <--
                StartCoroutine(Delay());
            }
        }

        if(allDead)
        {
            YouWon();
        }
    }

    private void YouWon()
    {
        //StopTheGameplay();
        _UIManager.WinPanelFadeIn();
        SaveUserProgress();
        allDead = false;
    }

    private void YouLose()
    {
        StartCoroutine(LoseSetup());
    }

    private void StopTheGameplay()
    {
        foreach (GameObject obj in disableThese)
        {
            obj.SetActive(false);
        }
        allDead = false;
        letsEnd = false;
    }

    private void SaveUserProgress()
    {
        SceneController.instance.SaveUnlockedLevel();
    }

    public void AddToList(GameObject kiddo)
    {
        _kidsList.Add(kiddo);
        kiddo.GetComponent<KidInfantryScript>().SetHitsToKill(hitsToKill);
    }

    public void HenryDoYourDance()
    {
        AudioManager.instance.Stop("Theme");
        AudioManager.instance.Play("Celebration");
        if (dancingHenry)
            dancingHenry.SetActive(true);
        else
            Debug.LogError("Dancing Henry not found!");
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(4f);
        allDead = true;
    }

    IEnumerator LoseSetup()
    {
        yield return new WaitForSeconds(1.5f);
        StopTheGameplay();
        _UIManager.LosePanelFadeIn();
    }
}