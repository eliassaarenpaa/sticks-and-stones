using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class LevelEventChannel : ScriptableObject
{
    public UnityEvent onStartNewLevel = new UnityEvent();
    public UnityEvent onStopLevel = new UnityEvent();

    public void StartNewLevel()
    {
        onStartNewLevel?.Invoke();
    }

    public void StopLevel()
    {
        onStopLevel?.Invoke();
    }

}

