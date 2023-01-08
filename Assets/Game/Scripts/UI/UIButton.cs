using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator animator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.Play("Hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.Play("Normal");
    }

}
