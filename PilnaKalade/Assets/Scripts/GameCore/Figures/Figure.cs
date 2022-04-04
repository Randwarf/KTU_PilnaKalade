using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Figure : MonoBehaviour {
    private Canvas canvas;
    public EventSystem m_EventSystem;
    public GraphicRaycaster m_Raycaster;

    void Start() {
        canvas = FindObjectOfType<Canvas>();
    }

    void Update() {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, Input.mousePosition,
            canvas.worldCamera,
            out Vector2 pos);

        transform.localPosition = pos;

        m_EventSystem = FindObjectOfType<EventSystem>();
        m_Raycaster = FindObjectOfType<GraphicRaycaster>();
        PointerEventData m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = transform.position;
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 1) {
            if (results[1].gameObject.CompareTag("Tile")) {
                results[1].gameObject.GetComponent<Image>().color = Color.yellow;
                Debug.Log("Hit " + results[1].gameObject.name);
            }
        }
    }
}
