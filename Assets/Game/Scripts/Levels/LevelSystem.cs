using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private LevelEventChannel levelEventChannel;
    [SerializeField] private GameStateEventChannel gameStateEventChannel;
    [SerializeField] private List<Level> allLevels;

    private Level _currentLevel;
    private int _currentLevelIndex = -1;

    private void OnEnable()
    {
        levelEventChannel.onStartNewLevel.AddListener(OnStartNextLevel);
        levelEventChannel.onStopLevel.AddListener(OnStopLevel);
        levelEventChannel.onCompleteLevel.AddListener(OnCompleteLevel);
        gameStateEventChannel.onGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnDisable()
    {
        levelEventChannel.onStartNewLevel.RemoveListener(OnStartNextLevel);
        levelEventChannel.onStopLevel.RemoveListener(OnStopLevel);
        levelEventChannel.onCompleteLevel.RemoveListener(OnCompleteLevel);
        gameStateEventChannel.onGameStateChanged.RemoveListener(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameState state)
    {
        if(state == GameState.MainMenu)
        {
            Restart();
        }
        else if (state == GameState.Failure)
        {
            OnStopLevel();
        }
    }

    private void OnStartNextLevel()
    {
        _currentLevelIndex++;

        if(_currentLevelIndex >= allLevels.Count)
        {
            Debug.Log("You Win, Thanks For Playing!");

            gameStateEventChannel.ChangeState(0);
            return;
        }

        _currentLevel = Instantiate(allLevels[_currentLevelIndex]);
        _currentLevel.StartLevel();
    }

    private void OnCompleteLevel()
    {
        OnStopLevel();
        gameStateEventChannel.ChangeState(2);
    }

    private void OnStopLevel()
    {
        if (_currentLevel?.gameObject)
        {
            Destroy(_currentLevel.gameObject);
        }
    }

    private void Restart()
    {
        OnStopLevel();
        _currentLevel = null;
        _currentLevelIndex = -1;
    }
}
