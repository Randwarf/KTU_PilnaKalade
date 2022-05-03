using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using TMPro;

public class CardManager : MonoBehaviour
{
    public int defaultHandSize = 4;
    public GameObject cardPrefab;

    public UnityEvent onNoCardsDrawnAfterDiscardChange = new UnityEvent();
    public UnityEvent<CardData> OnCardEndUse = new UnityEvent<CardData>();

    public bool NoCardsDrawnAfterDiscard { get; set; }

    private GameObject hand;


    void Awake()
    {
        hand = GameObject.Find("Hand");
        NoCardsDrawnAfterDiscard = true;

        var cards = GameObject.FindGameObjectsWithTag("Card")
            .Select(gameObject => gameObject.GetComponent<Card>());

        foreach(var card in cards)
            card.onUse.AddListener(OnCardUse);
    }

    public void DrawNewHand()
    {
        DrawNewHand(defaultHandSize);
    }

    public void DrawNewHand(int size)
    {
        DiscardHand();
        for (int i = 0; i < size; i++)
            DrawNewCard();
    }

    private void DrawNewCard()
    {
        GameObject card = Instantiate(cardPrefab, hand.transform);
        card.transform.localScale = card.transform.localScale * 0.15f;

        var cardComponent = card.GetComponent<Card>();
        cardComponent.onUse.AddListener(OnCardUse);
    }

    private void DiscardHand()
    {
        foreach (Transform child in hand.transform)
            Destroy(child.gameObject);

        NoCardsDrawnAfterDiscard = true;
        onNoCardsDrawnAfterDiscardChange.Invoke();
    }

    private void OnCardUse(CardData cardData)
    {
        NoCardsDrawnAfterDiscard = false;
        onNoCardsDrawnAfterDiscardChange.Invoke();

        OnCardEndUse.Invoke(cardData);
    }
}
