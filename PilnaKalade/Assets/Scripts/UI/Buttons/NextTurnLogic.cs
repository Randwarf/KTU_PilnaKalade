using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurnLogic : MonoBehaviour
{
    public void ProcessTurn(GameObject cardPrefab)
    {
        DrawNewHand(4, cardPrefab);
    }

    private void DrawNewHand(int handSize, GameObject cardPrefab)
    {
        DiscardHand();
        for(int i = 0; i < handSize; i++)
        {
            DrawNewCard(cardPrefab);
        }
    }

    private void DrawNewCard(GameObject cardPrefab)
    {
        //TODO: Update it to draw from deck instead of randomising
        var hand = GetHand().transform;
        GameObject card = Instantiate(cardPrefab, hand);
        card.transform.localScale = card.transform.localScale * 0.15f;

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
}
