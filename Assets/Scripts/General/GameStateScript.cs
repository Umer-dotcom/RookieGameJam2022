using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    ACTIVE,
    PAUSED,
    PASSED,
    LOST
}
public class GameStateScript : MonoBehaviour
{
    public static State state;
    private void Start()
    {
        state = State.ACTIVE;
    }

}
