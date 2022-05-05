using UnityEngine;
using UnityEngine.EventSystems;

public class CustomCheckbox : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool IsChecked { get; private set; } = false;

    public GameObject Checkmark;

    public void OnPointerUp(PointerEventData eventData) {
        IsChecked = !IsChecked;
        Checkmark.SetActive(IsChecked);
    }

    public void OnPointerDown(PointerEventData eventData) {
        // Without OnPointerDown, OnPointerUp is not working
    }
}
