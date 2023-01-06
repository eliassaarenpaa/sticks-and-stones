using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HomeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UIEventChannel uIEventChannel;
    [SerializeField] private PauseEventChannel pauseEventChannel;

    [SerializeField] private Animator homeButtonAnimator;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        _button.onClick.AddListener(GoToMenu);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        homeButtonAnimator.Play("Home Button Hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        homeButtonAnimator.Play("Home Button");
    }

    private void GoToMenu()
    {
        uIEventChannel.SwitchToCanvas("Menu");
        pauseEventChannel.TogglePause();
    }

}
