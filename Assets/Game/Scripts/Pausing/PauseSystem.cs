using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private UIEventChannel uiEventChannel;
    [SerializeField] private PauseEventChannel pauseEventChannel;

    private bool _isPaused;

    private void OnEnable()
    {
        pauseEventChannel.onTogglePause.AddListener(TogglePause);
    }

    private void OnDisable()
    {
        pauseEventChannel.onTogglePause.RemoveListener(TogglePause);
    }

    private void Update()
    {
        if (uiEventChannel._activeCanvasName == "Menu")
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;

        Time.timeScale = _isPaused ? 0f : 1f;

        pauseEventChannel.onPauseStateChanged?.Invoke(_isPaused);
    }
}
