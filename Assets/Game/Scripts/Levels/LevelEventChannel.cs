using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class LevelEventChannel : ScriptableObject
{
    public UnityEvent onStartNewLevel = new UnityEvent();
    public UnityEvent onStopLevel = new UnityEvent();
    public UnityEvent onCompleteLevel = new UnityEvent();

    public UnityEvent<int, int> onSetLevelCounter = new UnityEvent<int, int>();

    public UnityEvent<float> onSetLevelTimerForUI = new UnityEvent<float>();

    public void StartNewLevel()
    {
        onStartNewLevel?.Invoke();
    }

    public void StopLevel()
    {
        onStopLevel?.Invoke();
    }

    public void CompleteLevel()
    {
        onCompleteLevel?.Invoke();
    }

    public void SetLevelTimerForUI(float timeInSeconds)
    {
        onSetLevelTimerForUI?.Invoke(timeInSeconds);
    }

    public void SetLevelCounter(int currentLevel, int maxLevels)
    {
        onSetLevelCounter?.Invoke(currentLevel, maxLevels);
    }

}

