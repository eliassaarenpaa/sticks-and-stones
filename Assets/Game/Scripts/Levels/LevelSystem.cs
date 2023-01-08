using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private AudioSource levelSource;
    [SerializeField] private LevelEventChannel levelEventChannel;
    [SerializeField] private GameStateEventChannel gameStateEventChannel;
    [SerializeField] private List<Level> allLevels;

    private Level _currentLevel;
    private int _currentLevelIndex = -1;

    private void Start()
    {
        levelEventChannel.SetLevelCounter(_currentLevelIndex + 1, allLevels.Count);
    }

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
        else if (state == GameState.Fail)
        {
            OnStopLevel();
        }
        else if (state == GameState.Bunker)
        {
            levelEventChannel.SetLevelCounter(_currentLevelIndex + 1, allLevels.Count);
        }

    }

    private void OnStartNextLevel()
    {
        levelSource.Play();
        ClearItems();
        _currentLevelIndex++;
        _currentLevel = Instantiate(allLevels[_currentLevelIndex]);
        _currentLevel.StartLevel();
    }

    private void OnCompleteLevel()
    {
        OnStopLevel();

        if (_currentLevelIndex + 1 >= allLevels.Count)
        {
            Debug.Log("You Win, Thanks For Playing!");

            gameStateEventChannel.ChangeState(4);
        }
        else
        {
            gameStateEventChannel.ChangeState(2);
        }
    }

    private void OnStopLevel()
    {
        levelSource.Stop();
        ClearItems();

        if (_currentLevel)
        {
            if (_currentLevel.gameObject)
            {
                Destroy(_currentLevel.gameObject);
            }
        }

    }

    private void ClearItems()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            Destroy(item.gameObject);
        }
    }

    private void Restart()
    {
        OnStopLevel();
        _currentLevel = null;
        _currentLevelIndex = -1;
    }
}
