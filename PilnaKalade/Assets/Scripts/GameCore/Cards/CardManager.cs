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
        {
            DrawNewCard();
        }
    }

    private void DrawNewCard()
    {
        //TODO: Update it to draw from deck instead of randomising
        GameObject card = Instantiate(cardPrefab, hand.transform);
        card.transform.localScale = card.transform.localScale * 0.15f;
        card.GetComponent<Card>().onUse.AddListener(OnCardUse);

        string[] randomDescriptions = new string[] { "Lorem Ipsum", "Swing wildly", "Bottoms up!", "Slash", "Nothing personel, kiddo" };
        var description = card.transform.Find("Description Panel")
            .transform.Find("Description");
        int descriptionNumber = Random.Range(0, randomDescriptions.Length);
        description.GetComponent<TextMeshProUGUI>().text = randomDescriptions[descriptionNumber];

        var cost = card.transform.Find("Cost");
        cost.GetComponent<TextMeshProUGUI>().text = Random.Range(0, 6).ToString();
    }

    private void DiscardHand()
    {
        foreach (Transform child in hand.transform)
            Destroy(child.gameObject);

        NoCardsDrawnAfterDiscard = true;
        onNoCardsDrawnAfterDiscardChange.Invoke();
    }

    private void OnCardUse()
    {
        NoCardsDrawnAfterDiscard = false;
        onNoCardsDrawnAfterDiscardChange.Invoke();
    }
}
