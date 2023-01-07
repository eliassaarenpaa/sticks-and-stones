using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages the different states of the game
/// Is responsible for changing game state
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStateEventChannel gameStateEventChannel;
    [SerializeField] private GameState initialState;

    private GameState _currentState;

    private void Start()
    {
        gameStateEventChannel.changeGameState?.Invoke(initialState);
    }

    private void OnEnable()
    {
        gameStateEventChannel.changeGameState.AddListener(ChangeGameState);
    }

    private void OnDisable()
    {
        gameStateEventChannel.changeGameState.RemoveAllListeners();
    }

    private void ChangeGameState(GameState state)
    {
        if(state != _currentState)
        {
            _currentState = state;

            gameStateEventChannel.onGameStateChanged?.Invoke(state);
        }
    }
}
