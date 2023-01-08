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

    public GameState GetCurrentState => (GameState)currentState;

    /// <summary>
    /// -1 = Pause
    /// 0 = Menu,
    /// 1 = Game,
    /// 2 = Bunker,
    /// 3 = Fail
    /// 4 = Win
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
