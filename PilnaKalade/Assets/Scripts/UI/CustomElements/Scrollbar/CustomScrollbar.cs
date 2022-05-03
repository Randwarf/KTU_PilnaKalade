using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomScrollbar : MonoBehaviour
{
    public float scrollValue { get; set; }

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Image Fill;

    private float scrollbarHeight;
    private RectTransform rectTransform;

    void Start()
    {
        scrollRect.onValueChanged.AddListener((Vector2 value) => OnScrollbarValueChanged(value));
        rectTransform = Fill.GetComponent<RectTransform>();
        scrollbarHeight = GetComponent<RectTransform>().sizeDelta.y;
    }

    void Update()
    {
        float width = rectTransform.sizeDelta.x;
        rectTransform.sizeDelta = new Vector2(width, scrollValue * scrollbarHeight);
    }

    private void OnScrollbarValueChanged(Vector2 value) {
        Debug.Log(value);
        scrollValue = 1 - Mathf.Clamp(value.y, 0, 1);
    }
}
