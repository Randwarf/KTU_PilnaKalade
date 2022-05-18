using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public int Value = 100;
    public int MaxValue = 100;

    [SerializeField] private Transform Background;
    [SerializeField] private Transform OuterOutline;
    [SerializeField] private float MinPadding = 180;
    [SerializeField] private Text TextValue;

    private RectTransform backgroundRectTransform;
    private RectTransform outlineRectTransform;
    private Vector2 outlineMaxSize;
    private Vector2 backgroundMaxSize;

    void Awake()
    {
        backgroundRectTransform = Background.GetComponent<RectTransform>();
        outlineRectTransform = OuterOutline.GetComponent<RectTransform>();
        outlineMaxSize = outlineRectTransform.sizeDelta;
        backgroundMaxSize = backgroundRectTransform.sizeDelta;
    }

    public void SetValue(float value) {
        Value = Mathf.Clamp((int)value, 0, 100);
        float currentSizeX = Value * (outlineMaxSize.x - MinPadding) / 100;
        outlineRectTransform.sizeDelta = new Vector2(MinPadding + currentSizeX, outlineMaxSize.y);
        TextValue.text = Value.ToString();
    }

    public void SetMaxValue(float value) {
        float currentSizeX = value * (backgroundMaxSize.x - MinPadding) / 100;
        backgroundRectTransform.sizeDelta = new Vector2(MinPadding + currentSizeX, backgroundMaxSize.y);
    }

    private void Update() {
        SetValue(Value);

        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    SetMaxValue(TestBGValue);
        //}
    }
}
