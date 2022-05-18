using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomCheckbox : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool isOn { get; set; } = true;

    public GameObject Checkmark;

    public UnityEvent OnValueChanged;

    public void OnPointerUp(PointerEventData eventData) {
        isOn = !isOn;
        Checkmark.SetActive(isOn);
        OnValueChanged?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData) {
        // Without OnPointerDown, OnPointerUp is not working
    }

    public void SetToggleWithoutEffects(bool value) {
        Debug.Log($"Got value {value}");
        isOn = value;
        Checkmark.SetActive(value);
    }

    public void SetToggle(bool value) {
        if (isOn != value) {
            OnValueChanged?.Invoke();
        }
        isOn = value;
    }
}
