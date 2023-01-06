using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TogglePauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator pauseButtonAnimator;
    [SerializeField] private PauseEventChannel pauseEventChannel;

    private bool _isPaused;

    private void OnEnable()
    {
        pauseEventChannel.onPauseStateChanged.AddListener(ChangeAnimation);
    }

    private void OnDisable()
    {
        pauseEventChannel.onPauseStateChanged.RemoveListener(ChangeAnimation);
    }

    private void ChangeAnimation(bool isPaused)
    {
        _isPaused = isPaused;

        pauseButtonAnimator.Play(_isPaused ? "Unpaused" : "Paused");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isPaused)
        {
            pauseButtonAnimator.Play("Paused Hover");
        }
        else
        {
            pauseButtonAnimator.Play("Unpaused Hover");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pauseButtonAnimator.Play(_isPaused ? "Unpaused" : "Paused");
    }
}
