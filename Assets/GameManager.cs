using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager _UIManager;
    [SerializeField]
    private KidSpawnerScript kidSpawnerScript;
    [SerializeField]
    private GameObject[] disableThese;
    [SerializeField]
    private List<GameObject> kids2;
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
    int removedKiddos = 0;

    private void Start()
    {
        letsEnd = true;
        totalKids = kidSpawnerScript.maxKidCount + shooterKids;
    }

    private void Update()
    {
        if(letsEnd)
        {
            for(int i = 0; i < kids2.Count; i++)
            {
                if (kids2[i].GetComponent<Collider>().enabled == false)
                {
                    removedKiddos++;
                    _UIManager.inc = true;
                    kids2.Remove(kids2[i]);
                }
            }
            
            if(removedKiddos == totalKids)
            {
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
        StopTheGameplay();
        _UIManager.WinPanelFadeIn();
        SaveUserProgress();
    }

    private void YouLose()
    {
        StopTheGameplay();
        _UIManager.loseFadeIN = true;
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
        kids2.Add(kiddo);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        allDead = true;
    }
}