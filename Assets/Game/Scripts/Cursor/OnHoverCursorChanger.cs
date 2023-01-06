using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverCursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    [SerializeField] private Texture2D hoverCursor;
    [SerializeField] private Texture2D defaultCursor;


    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
