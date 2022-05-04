using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTurnButton : MonoBehaviour
{
    private CardManager cardManager;
    private CustomButton nextTurnButton;

    void Awake()
    {
        cardManager = GameObject.FindWithTag("CardManager").GetComponent<CardManager>();
        nextTurnButton = GetComponent<CustomButton>();
    }

    void OnEnable()
    {
        nextTurnButton.OnPressed.AddListener(OnNextTurnClick);
    }

    void OnDisable()
    {
        nextTurnButton.OnPressed.RemoveListener(OnNextTurnClick);
    }

    private void OnNextTurnClick()
    {
        cardManager.DrawNewHand();
    }
}
