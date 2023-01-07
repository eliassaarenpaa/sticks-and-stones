using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class GameStateEventChannel : ScriptableObject
{
    /// <summary>
    /// GameManager writes
    /// </summary>
    public UnityEvent<GameState> changeGameState = new UnityEvent<GameState>();
    
    /// <summary>
    /// Others read 
    /// </summary>
    public UnityEvent<GameState> onGameStateChanged = new UnityEvent<GameState>();

    private int currentState = 0;
    private int previousState = 0;

    /// <summary>
    /// -1 = Pause (call toggles it)
    /// 0 = Menu,
    /// 1 = Game,
    /// 2 = Bunker
    /// 
    /// 
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(int state)
    {
        previousState = currentState;
        currentState = state;
        changeGameState?.Invoke((GameState)state);
    }

    public void ChangeToPreviousState()
    {
        ChangeState(previousState);
    }

}
