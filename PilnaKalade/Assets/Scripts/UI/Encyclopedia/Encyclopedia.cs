using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encyclopedia : MonoBehaviour
{
    public GameObject CardPrefab;
    public Transform CardGrid;

    void Start()
    {
        DisplayCards();
    }

    private void DisplayCards() {
        List<CardData> cards = CardDatabase.GetCards();
        foreach (CardData cardData in cards) {
            GameObject cardGO = Instantiate(CardPrefab, CardGrid);
            Card card = cardGO.GetComponent<Card>();
            card.SetData(cardData);
            card.UpdateVisuals();
        }
    }
}