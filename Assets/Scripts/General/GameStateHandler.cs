using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ComboSystem.Scripts;
public class GameStateHandler : MonoBehaviour
{
    [SerializeField] int infantryTotal;
    [SerializeField] int throwerTotal;
    public GameState gameState;
    public WindowState windowState;

    public float comboCooldown = 2f;
    private float comboTimeTracker = 0;
    //ComboCounter combo;
    bool onACombo = false;
    private int score = 0;

    int remainingInfantry;
    int remainingThrowers;
    private void Start()
    {
        remainingInfantry = infantryTotal;
        remainingThrowers = throwerTotal;
        //combo = GetComponent<ComboCounter>();
        score = 0;
        gameState = GameState.PRESTART;
        windowState = WindowState.RUNNING;
    }
    private void OnEnable()
    {
        Bullets.HitEvent += OnKidHit;
        PlayerScript.playerDeathEvent += GameLost;
        KidInfantryScript.InfantryKilledEvent += InfantryKilled;
        KidThrowerScript.ThrowerKilledEvent += ThrowerKilled;
    }
    private void OnDisable()
    {
        Bullets.HitEvent -= OnKidHit;
        PlayerScript.playerDeathEvent -= GameLost;
        KidInfantryScript.InfantryKilledEvent -= InfantryKilled;
        KidThrowerScript.ThrowerKilledEvent -= ThrowerKilled;
    }
    void OnKidHit(int value)
    {
        //combo.Increment();
        score += value * 10;
        onACombo = true;
    }

    void InfantryKilled()
    {
        remainingInfantry--;
        if (remainingInfantry <= 0 && remainingThrowers <= 0) GameWon();
    }
    void ThrowerKilled()
    {
        remainingThrowers--;
        if (remainingThrowers <= 0 && remainingInfantry <= 0) GameWon();
    }
    private void Update()
    {
        comboTimeTracker += Time.deltaTime;

        if (onACombo && comboTimeTracker >= comboCooldown)
        {
            //combo.Break();
            onACombo = false;
        }
    }
    private void GamePaused()
    {
        
    }
    private void GameLost()
    {
        Debug.Log("Game Lost. Score = "+ score);
    }
    private void GameWon()
    {
        Debug.Log("Game Won!! Score = " + score);
    }
}
