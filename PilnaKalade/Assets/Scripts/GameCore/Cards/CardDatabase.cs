using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardDatabase
{
    private static List<CardData> cards;

    private const string CARDS_DATA_FILE = "Cards.json";

    public static List<CardData> GetCards() {
        return cards;
    }

    public static CardData GetCard(int index)
    {
        return cards[index];
    }

    public static int GetCount()
    {
        return cards.Count;
    }

    public static void LoadCards() {
        string jsonName = CARDS_DATA_FILE.Split('.')[0];
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonName);
        // Making json object out of array
        var json = jsonFile.text;
        json = "{\"cards\":" + json + "}";
        Root jsonObject = JsonUtility.FromJson<Root>(json);
        cards = jsonObject.cards;
    }

    [Serializable]
    private class Root {
        public List<CardData> cards;
    }
}
