using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class PauseEventChannel : ScriptableObject
{
    public UnityEvent onTogglePause = new UnityEvent();
    public UnityEvent<bool> onPauseStateChanged = new UnityEvent<bool>();
    
    public void TogglePause()
    {
        onTogglePause?.Invoke();
    }

}
