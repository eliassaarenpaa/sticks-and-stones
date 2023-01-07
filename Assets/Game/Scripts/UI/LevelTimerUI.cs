using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class LevelTimerUI : MonoBehaviour
{
    [SerializeField] private LevelEventChannel levelEventChannel;
    [SerializeField] private TextMeshProUGUI timerValueUGUI;

    private Coroutine _timerRoutine;

    private void OnEnable()
    {
        levelEventChannel.onSetLevelTimerForUI.AddListener(OnSetLevelTimer);
    }

    private void OnDisable()
    {
        levelEventChannel.onSetLevelTimerForUI.RemoveListener(OnSetLevelTimer);
    }

    private void OnSetLevelTimer(float duration)
    {
        if(_timerRoutine != null)
        {
            StopCoroutine(_timerRoutine);
        }

        _timerRoutine = StartCoroutine(TimerRoutine(duration));
    }

    private IEnumerator TimerRoutine(float duration)
    {
        var dur = duration;
        while (dur > 0)
        {
            TimeSpan t = TimeSpan.FromSeconds(dur);
            timerValueUGUI.text = $"{t.Minutes}:{t.Seconds}";

            dur -= Time.deltaTime;
            yield return null;
        }
    }
}
