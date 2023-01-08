using TMPro;
using UnityEngine;

public class BunkerLevelUI : MonoBehaviour
{
    [SerializeField] private LevelEventChannel levelEventChannel;
    [SerializeField] private TextMeshProUGUI levelCountUGUI;

    private void OnEnable()
    {
        levelEventChannel.onSetLevelCounter.AddListener(OnSetLevelCounter);
    }

    private void OnDisable()
    {
        levelEventChannel.onSetLevelCounter.RemoveListener(OnSetLevelCounter);
    }

    private void OnSetLevelCounter(int currentLevel, int maxLevels)
    {
        levelCountUGUI.text = $"{currentLevel}/{maxLevels}";
    }
}
