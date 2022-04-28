using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    private static List<CardData> deck = new List<CardData>();
    private int x;
    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        CardDatabase.LoadCards();
        int count = CardDatabase.GetCount();
        for (int i = 0; i < 20; i++)
        {
            x = Random.Range(0, count);
            deck.Add(CardDatabase.GetCard(x));
        }
    }
    public static List<CardData> GetDeck()
    {
        return deck;
    }

    public static CardData DrawRandom()
    {
        int randomIndex = Random.Range(0, deck.Count);
        return deck[randomIndex];
    }

    // Update is called once per frame
    void Update()
    {

    }
}