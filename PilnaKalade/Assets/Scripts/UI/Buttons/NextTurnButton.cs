using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTurnButton : MonoBehaviour
{
    private CardManager cardManager;
    private Button nextTurnButton;

    void Awake()
    {
        cardManager = GameObject.FindWithTag("CardManager").GetComponent<CardManager>();
        nextTurnButton = GetComponent<Button>();
    }

    void OnEnable()
    {
        nextTurnButton.onClick.AddListener(OnNextTurnClick);
    }

    void OnDisable()
    {
        nextTurnButton.onClick.RemoveListener(OnNextTurnClick);
    }

    private void OnNextTurnClick()
    {
        cardManager.DrawNewHand();
    }
}
