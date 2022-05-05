using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomDropdown : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Text selectedOptionText;
    [SerializeField] private Transform optionsParent;
    [SerializeField] private GameObject optionPrefab;

    [SerializeField] private List<string> options;

    public int SelectedIndex { get; private set; } = -1;

    private bool opened = false;

    public void SelectOption(int index, string text) {
        opened = false;
        selectedOptionText.text = text;
        SelectedIndex = index;
        DestroyOptions();
    }

    public void DestroyOptions() {
        foreach (Transform option in optionsParent) {
            if (option.GetSiblingIndex() > 0) {
                Destroy(option.gameObject);
            }
        }
    }

    public void SpawnOptions() {
        for (int i = 0; i < options.Count; i++) {
            GameObject optionGO = Instantiate(optionPrefab, optionsParent);
            CustomDropdownOption optionScript = optionGO.GetComponent<CustomDropdownOption>();
            optionScript.Initialize(this, options[i], i);
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        opened = !opened;
        if (!opened) DestroyOptions();
        else SpawnOptions();
    }

    public void OnPointerDown(PointerEventData eventData) {
        // Without OnPointerDown, OnPointerUp is not working
    }
}
