using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDeck
{
    private static List<CardData> deck = new List<CardData>();

    public static void LoadIfUnloaded() {
        if(deck.Count == 0) {
            for (int i = 0; i < 20; i++)
            {
                deck.Add(CardDatabase.GetRandomCard());
            }
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
}