using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleAudioButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator audioButtonAnimator;
    [SerializeField] private AudioEventChannel audioEventChannel;

    private bool _audioIsOn;

    private void OnEnable()
    {
        audioEventChannel.onAudioStateChanged.AddListener(ChangeAnimation);
    }

    private void OnDisable()
    {
        audioEventChannel.onAudioStateChanged.RemoveListener(ChangeAnimation);
    }

    private void ChangeAnimation(bool isOn)
    {
        _audioIsOn = isOn;

        audioButtonAnimator.Play(_audioIsOn ? "Sound On" : "Sound Off");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_audioIsOn)
        {
            audioButtonAnimator.Play("Music Icon Hover");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        audioButtonAnimator.Play(_audioIsOn ? "Sound On" : "Sound Off");
    }
}
