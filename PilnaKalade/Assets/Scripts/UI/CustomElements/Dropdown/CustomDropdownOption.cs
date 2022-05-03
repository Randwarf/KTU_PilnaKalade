using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomDropdownOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Color primaryColor = new Color(30, 167, 225);
    [SerializeField] private Color selectedTextColor = new Color(78, 78, 78);
    [SerializeField] private Text optionText;
    [SerializeField] private Image selectionBackground;

    private CustomDropdown dropdown;
    private int index;

    public void Initialize(CustomDropdown dropdown, string optionName, int index) {
        this.dropdown = dropdown;
        optionText.text = optionName;
        this.index = index;
    }

    public void OnPointerUp(PointerEventData eventData) {
        dropdown.SelectOption(index, optionText.text);
    }

    public void OnPointerDown(PointerEventData eventData) {
        // Without OnPointerDown, OnPointerUp is not working
    }

    public void OnPointerEnter(PointerEventData eventData) {
        selectionBackground.DOColor(primaryColor, 0.2f);
        optionText.DOColor(Color.white, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData) {
        selectionBackground.DOFade(0f, 0.2f);
        optionText.DOColor(selectedTextColor, 0.2f);
    }

    private void OnDestroy() {
        DOTween.Kill(selectionBackground);
        DOTween.Kill(optionText);
    }
}
