using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardDatabase
{
    private static List<CardData> cards = new List<CardData>();

    private const string CARDS_DATA_FILE = "Cards.json";
    private const string CARDS_PREFS_KEY = "Cards";

    public static void LoadCards() {
        string jsonString = "";

        if(!PlayerPrefs.HasKey(CARDS_PREFS_KEY)) {
            string jsonName = CARDS_DATA_FILE.Split('.')[0];
            TextAsset jsonFile = Resources.Load<TextAsset>(jsonName);
            // Making json object out of array
            jsonString = jsonFile.text;
            jsonString = "{\"cards\":" + jsonString + "}";
        } else {
            jsonString = PlayerPrefs.GetString(CARDS_PREFS_KEY);
        }

        Root jsonObject = JsonUtility.FromJson<Root>(jsonString);
        cards = jsonObject.cards;
    }

    public static void AddNewCard(CardData card) 
    {
        cards.Add(card);

        PlayerPrefs.SetString(CARDS_PREFS_KEY, 
            JsonUtility.ToJson(
                new Root() { cards = cards }));
        PlayerPrefs.Save();
    }

    public static List<CardData> GetCards() 
    {
        LoadCardsIfUnloaded();
        return cards;
    }

    public static CardData GetRandomCard() 
    {
        LoadCardsIfUnloaded();
        return cards[UnityEngine.Random.Range(0, cards.Count)];
    }

    private static void LoadCardsIfUnloaded() {
        if(cards.Count == 0) {
            LoadCards();
        }
    }

    [Serializable]
    private class Root {
        public List<CardData> cards;
    }
}
