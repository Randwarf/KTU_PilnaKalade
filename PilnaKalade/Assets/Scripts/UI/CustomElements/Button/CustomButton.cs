using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int ShadowHeight = 8;
    public Transform Outline;

    public UnityEvent OnPressed;

    public void OnPointerDown(PointerEventData eventData) {
        Outline.DOLocalMoveY(Outline.localPosition.y - ShadowHeight, 0f);
    }

    public void OnPointerUp(PointerEventData eventData) {
        OnPressed?.Invoke();
        Outline.DOLocalMoveY(Outline.localPosition.y + ShadowHeight, 0f);
    }
}
