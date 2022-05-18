using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomCheckbox : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool isOn { get; private set; }

    public GameObject Checkmark;

    public UnityEvent OnValueChanged;

    private void Start() {
        isOn = true;
        Checkmark.SetActive(isOn);
    }

    public void OnPointerUp(PointerEventData eventData) {
        isOn = !isOn;
        Checkmark.SetActive(isOn);
        OnValueChanged?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData) {
        // Without OnPointerDown, OnPointerUp is not working
    }

    public void SetToggle(bool value) {
        if (isOn != value) {
            OnValueChanged?.Invoke();
        }
        isOn = value;
    }
}
