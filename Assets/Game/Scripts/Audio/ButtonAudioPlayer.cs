using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class ButtonAudioPlayer : MonoBehaviour, IPointerEnterHandler
{
    private Button _button;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip hoverSound;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.volume = 1.0f;
        _audioSource.pitch = 1.0f;
        _audioSource.playOnAwake = false;
        _button.onClick.AddListener(PlaySoundOnClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void PlaySoundOnClick()
    {
        _audioSource.clip = clickSound;
        _audioSource.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _audioSource.clip = hoverSound;
        _audioSource.Play();
    }
}
