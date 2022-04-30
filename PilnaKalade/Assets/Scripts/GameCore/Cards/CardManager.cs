using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

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
        GameObject.FindGameObjectsWithTag("Card")
            .Select(gameObject => gameObject.GetComponent<Card>())
            .ToList()
            .ForEach(card => card.onUse.AddListener(OnCardUse));
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
        var handTransform = hand.transform;
        GameObject card = Instantiate(cardPrefab, handTransform);
        card.transform.localScale = card.transform.localScale * 0.15f;
        card.GetComponent<Card>().onUse.AddListener(OnCardUse);

        string[] randomDescriptions = new string[] { "Lorem Ipsum", "Swing wildly", "Bottoms up!", "Slash", "Nothing personel, kiddo" };
        var desc = card.transform.Find("Description Panel");
        desc = desc.transform.Find("Description");
        int descriptionNumber = UnityEngine.Random.Range(0, randomDescriptions.Length);
        desc.GetComponent<TMPro.TextMeshProUGUI>().text = randomDescriptions[descriptionNumber];

        var cost = card.transform.Find("Cost");
        cost.GetComponent<TMPro.TextMeshProUGUI>().text = UnityEngine.Random.Range(0, 6).ToString();
    }

    private void DiscardHand()
    {
        var handTransform = hand.transform;
        foreach (Transform child in handTransform)
        {
            GameObject.Destroy(child.gameObject);
        }

        NoCardsDrawnAfterDiscard = true;
        onNoCardsDrawnAfterDiscardChange.Invoke();
    }

    private void OnCardUse()
    {
        NoCardsDrawnAfterDiscard = false;
        onNoCardsDrawnAfterDiscardChange.Invoke();
    }
}