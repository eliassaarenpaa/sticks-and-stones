using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private UIEventChannel uiEventChannel;
    [SerializeField] private GameStateEventChannel gameStateEventChannel;

    private bool _isPaused;

    private void OnEnable()
    {
        gameStateEventChannel.onGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnDisable()
    {
        gameStateEventChannel.onGameStateChanged.RemoveListener(OnGameStateChanged);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(gameStateEventChannel.GetCurrentState == GameState.Paused || gameStateEventChannel.GetCurrentState == GameState.Game)
            {
                if (!_isPaused)
                {
                    gameStateEventChannel.ChangeState(-1);
                }
                else
                {
                    gameStateEventChannel.ChangeToPreviousState();
                }
            }

        }
    }
    private void OnGameStateChanged(GameState state)
    {
        SetPause(state == GameState.Paused);
    }

    private void SetPause(bool value)
    {
        _isPaused = value;

        Time.timeScale = _isPaused ? 0f : 1f;
    }
}
