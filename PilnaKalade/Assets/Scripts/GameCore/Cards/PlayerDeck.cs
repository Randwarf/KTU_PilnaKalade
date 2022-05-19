using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlayerDeck
{
    const int STARTING_HAND_SIZE = 5;
    const string CARDS_PREFS_KEY = "Deck";
    private static List<CardData> deck = new List<CardData>();

    public static void LoadIfUnloaded() {
        if (deck.Count == 0) {

            if (!PlayerPrefs.HasKey(CARDS_PREFS_KEY))
            {
                for (int i = 0; i < STARTING_HAND_SIZE; i++)
                {
                    deck.Add(CardDatabase.GetRandomCard());
                }
            }
            else
            {
                var jsonString = PlayerPrefs.GetString(CARDS_PREFS_KEY);
                Root jsonObject = JsonUtility.FromJson<Root>(jsonString);
                deck = jsonObject.cards;
            }
        }
    }

    public static List<CardData> GetDeck()
    {
        return deck;
    }

    public static void SetDeck(List<CardData> newDeck)
    {
        deck.Clear();
        foreach(CardData card in newDeck)
        {
            deck.Add(card);
        }
    }

    public static CardData DrawRandom()
    {
        int randomIndex = UnityEngine.Random.Range(0, deck.Count);
        return deck[randomIndex];
    }

    public static void AddCard(CardData card)
    {
        deck.Add(card);

        PlayerPrefs.SetString(CARDS_PREFS_KEY,
            JsonUtility.ToJson(
                new Root() { cards = deck }));
        PlayerPrefs.Save();
    }

    public static void Clear()
    {
        deck.Clear();
    }

    [Serializable]
    private class Root
    {
        public List<CardData> cards;
    }
}