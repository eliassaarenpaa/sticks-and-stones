using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator homeButtonAnimator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        homeButtonAnimator.Play("Home Button Hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        homeButtonAnimator.Play("Home Button");
    }

}
