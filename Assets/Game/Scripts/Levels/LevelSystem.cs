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
        levelEventChannel.onStartNewLevel.AddListener(StartNextLevel);
        levelEventChannel.onStopLevel.AddListener(StopLevel);

        gameStateEventChannel.onGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnDisable()
    {
        levelEventChannel.onStartNewLevel.RemoveListener(StartNextLevel);
        levelEventChannel.onStopLevel.RemoveListener(StopLevel);

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
            StopLevel();
        }
    }

    private void StartNextLevel()
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

    private void StopLevel()
    {
        if (_currentLevel?.gameObject)
        {
            Destroy(_currentLevel.gameObject);
        }
    }

    private void Restart()
    {
        StopLevel();
        _currentLevel = null;
        _currentLevelIndex = -1;
    }
}
