using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public int Value { get; private set; } = 100;
    
    [SerializeField] private float MinPadding = 180;
    [SerializeField] private Text TextValue;
    
    private RectTransform rectTransform;
    private Vector2 maxSize;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        maxSize = rectTransform.sizeDelta;
    }

    public void SetValue(int value) {
        Value = value;
        float currentSizeX = Value * (maxSize.x - MinPadding) / 100;
        rectTransform.sizeDelta = new Vector2(MinPadding + currentSizeX, maxSize.y);
        TextValue.text = Value.ToString();
    }
}
