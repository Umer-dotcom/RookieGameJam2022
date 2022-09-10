using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    public GameState gameState;
    public WindowState windowState;

    private int score;

    private void Start()
    {
        score = 0;
        gameState = GameState.PRESTART;
        windowState = WindowState.RUNNING;
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    private void Update()
    {
        
    }
    private void GamePaused()
    {
        
    }
    private void GameLost()
    {

    }
    private void GameWon()
    {

    }

}
