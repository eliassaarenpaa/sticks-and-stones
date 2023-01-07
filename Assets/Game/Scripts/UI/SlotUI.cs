using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void UpdateUI(Sprite sprite)
    {
        _image.enabled = sprite != null;
        _image.sprite = sprite;
    }
}
