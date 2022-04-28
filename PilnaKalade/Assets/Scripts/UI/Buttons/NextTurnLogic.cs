using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurnLogic : MonoBehaviour
{
    public GameObject cardPrefab;

    public void ProcessTurn()
    {
        DrawNewHand(4);
    }

    private void DrawNewHand(int handSize)
    {
        DiscardHand();
        for(int i = 0; i < handSize; i++)
        {
            DrawNewCard(PlayerDeck.DrawRandom());
        }
    }

    private void DrawNewCard(CardData data)
    {
        var hand = GetHand().transform;
        GameObject card = Instantiate(cardPrefab, hand);
        card.transform.localScale = card.transform.localScale * 0.15f;
        var cardScript = card.GetComponent<Card>();
        cardScript.SetData(data);
        cardScript.UpdateVisuals();
    }

    private void DiscardHand()
    {
        var hand = GetHand().transform;
        foreach (Transform child in hand)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private GameObject GetHand()
    {
        return GameObject.Find("Hand");
    }

    public void Start()
    {
        DrawNewHand(4);
    }
}
