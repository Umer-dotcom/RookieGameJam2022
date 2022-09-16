using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private UIManager _UIManager;
    //[SerializeField]
    //private KidSpawnerScript kidSpawnerScript;
    [SerializeField]
    private GameObject[] disableThese;
    //[SerializeField]
    //private List<GameObject> _kidsList;
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
    private void OnEnable()
    {
        PlayerScript.playerDeathEvent += YouLose;
        KidInfantryScript.InfantryKilledEvent += KidRemoved;
        KidThrowerScript.ThrowerKilledEvent += KidRemoved;
    }
    private void OnDisable()
    {
        PlayerScript.playerDeathEvent -= YouLose;
        KidInfantryScript.InfantryKilledEvent -= KidRemoved;
        KidThrowerScript.ThrowerKilledEvent -= KidRemoved;
    }
    private void KidRemoved()
    {
        removedKiddos++;
        _UIManager.inc = true;
        if (removedKiddos >= totalKids)
        {
            StartCoroutine(WinWithDelay());
        }
    }
    private void Update()
    {
        //if (_player.GetComponent<Collider>().enabled == false && isPlayerDead == false)
        //{
        //    isPlayerDead = true;
        //    YouLose();
        //}

        //if(letsEnd)
        //{
        //    for(int i = 0; i < _kidsList.Count; i++)
        //    {
        //        if (_kidsList[i].GetComponent<Collider>().enabled == false)
        //        {
        //            removedKiddos++;
        //            _UIManager.inc = true;
        //            _kidsList.Remove(_kidsList[i]);
        //        }
        //    }
            
        //    if(removedKiddos == totalKids)
        //    {
        //        StartCoroutine(Delay());
        //    }
        //}

        //if(allDead)
        //{
        //    YouWon();
        //}
    }

    private void YouWon()
    {
        StopTheGameplay();
        _UIManager.WinPanelFadeIn();
        SaveUserProgress();
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
        //_kidsList.Add(kiddo);
    }

    IEnumerator WinWithDelay()
    {
        yield return new WaitForSeconds(1f);
        allDead = true;
        YouWon();
    }

    IEnumerator LoseSetup()
    {
        yield return new WaitForSeconds(1.5f);
        StopTheGameplay();
        _UIManager.LosePanelFadeIn();
    }
}