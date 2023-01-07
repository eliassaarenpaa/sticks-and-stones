using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private GameStateEventChannel gameStateEventChannel;
    [SerializeField] private List<Level> allLevels;

    private Level _currentLevel;
    private int _currentLevelIndex = -1;

    private void OnEnable()
    {
        gameStateEventChannel.onGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnDisable()
    {
        gameStateEventChannel.onGameStateChanged.RemoveListener(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameState state)
    {
        if(state == GameState.Game)
        {
            StartNextLevel();
        }
        else if(state == GameState.MainMenu)
        {
            Restart();
        }
        else 
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
