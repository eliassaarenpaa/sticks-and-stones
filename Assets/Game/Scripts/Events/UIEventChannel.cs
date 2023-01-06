using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class UIEventChannel : ScriptableObject
{
    public UnityEvent<string> onSwitchToCanvas = new UnityEvent<string>();

    public string _activeCanvasName;

    public void SwitchToCanvas(string name)
    {
        _activeCanvasName = name;
        onSwitchToCanvas?.Invoke(name);
    }
}
