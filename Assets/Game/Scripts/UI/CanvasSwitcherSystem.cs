using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSwitcherSystem : MonoBehaviour
{
    [SerializeField] private AudioSource canvasSwitchSource;
    [SerializeField] private AudioSource failSource;
    [SerializeField] private AudioSource winSource;
    [SerializeField] private AudioSource menuSource;

    [SerializeField] private GameStateEventChannel gameStateEventChannel;
    [SerializeField] private UIEventChannel uIEventChannel;
    [SerializeField] private float curtainFadeInDuration;
    [SerializeField] private float curtainFadeOutDuration;

    [SerializeField] private string defaultCanvasName;
    [SerializeField] private string curtainCanvasName;

    [SerializeField] private CanvasObject curtain;

    [SerializeField] private List<CanvasObject> canvasObjects;

    private CanvasObject _activeCanvasObject;

    private CanvasObject _lastCanvasObject;

    private void Awake()
    {
        _activeCanvasObject = null;

        DisableAllCanvasObjects();
        SwitchToCanvas(defaultCanvasName);
    }
    private void OnEnable()
    {
        uIEventChannel.onSwitchToCanvas.AddListener(SwitchToCanvas);
        gameStateEventChannel.onGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnDisable()
    {
        uIEventChannel.onSwitchToCanvas.RemoveListener(SwitchToCanvas);
        gameStateEventChannel.onGameStateChanged.RemoveListener(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameState state)
    {
        if(state == GameState.MainMenu || state == GameState.Bunker)
        {
            menuSource.Play();
        }
        else
        {
            menuSource.Stop();
        }

        switch (state)
        {
            case GameState.Paused:
                canvasSwitchSource.Play();
                SwitchToCanvas("Pause");
                break;
            case GameState.MainMenu:
                canvasSwitchSource.Play();
                
                SwitchToCanvas("Menu");
                break;
            case GameState.Game:
                canvasSwitchSource.Play();
                SwitchToCanvas("Game");
                break;
            case GameState.Bunker:
                canvasSwitchSource.Play();
                SwitchToCanvas("Bunker");
                break;
            case GameState.Fail:
                failSource.Play();
                SwitchToCanvas("Fail");
                break;
            case GameState.Win:
                winSource.Play();
                SwitchToCanvas("Win");
                break;
        }
    }

    public void SwitchToCanvas(string canvasName)
    {
        var newCanvasObject = GetCanvasObject(canvasName);

        if (!CanvasObjectExists(newCanvasObject))
        {
            return;
        }

        if (CanvasObjectExists(_activeCanvasObject))
        {
            _lastCanvasObject = _activeCanvasObject;

            if (_activeCanvasObject.canvasName.Equals(canvasName))
            {
                return;
            }

            FadeInCurtain(() =>
            {
                DisableAllCanvasObjects();

                EnableCanvas(newCanvasObject);
                FadeOutCurtain();
            });
        }
        else
        {
            FadeInCurtain(() =>
            {
                EnableCanvas(newCanvasObject);
                FadeOutCurtain();
            });
        }

        _activeCanvasObject = newCanvasObject;
    }

    private void FadeInCurtain(Action callback)
    {
        curtain.canvas.enabled = true;
        curtain.group.blocksRaycasts = true;
        curtain.group.alpha = 0;
        curtain.group.DOKill();
        curtain.group.DOFade(1, curtainFadeInDuration).SetUpdate(true).OnComplete(() =>
        {
            callback?.Invoke();
        });
    }

    private void FadeOutCurtain()
    {
        curtain.group.blocksRaycasts = false;
        curtain.group.DOKill();

        curtain.group.DOFade(0, curtainFadeOutDuration).SetUpdate(true).OnComplete(() =>
        {
            curtain.canvas.enabled = false;
        });
    }

    private void EnableCanvas(CanvasObject canvasObject)
    {
        canvasObject.canvas.enabled = true;
        canvasObject.group.alpha = 1;
        canvasObject.group.interactable = true;
        canvasObject.group.blocksRaycasts = true;
    }

    private void DisableCanvas(CanvasObject canvasObject)
    {
        canvasObject.canvas.enabled = false;
        canvasObject.group.alpha = 0;
        canvasObject.group.interactable = false;
        canvasObject.group.blocksRaycasts = false;
    }

    private void DisableAllCanvasObjects()
    {
        foreach (var canvasNameObject in canvasObjects)
        {
            DisableCanvas(canvasNameObject);
        }
    }

    private CanvasObject GetCanvasObject(string canvasName)
    {
        return canvasObjects.GetValue(x => x.canvasName.Equals(canvasName)) ?? null;
    }

    private bool CanvasObjectExists(CanvasObject canvasObject)
    {
        return canvasObject != null && canvasObject.canvas != null;
    }
}
