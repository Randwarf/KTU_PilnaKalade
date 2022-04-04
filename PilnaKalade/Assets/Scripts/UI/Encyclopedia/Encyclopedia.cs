using DG.Tweening;
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

    public void Open() {
        transform.DOScale(1, 0.2f);
    }

    public void Close() {
        transform.DOScale(0, 0.2f);
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