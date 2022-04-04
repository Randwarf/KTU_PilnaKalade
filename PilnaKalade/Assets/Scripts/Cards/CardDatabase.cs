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

    public static void LoadCards() {
        string jsonPath = $"{Application.dataPath}/Resources/{CARDS_DATA_FILE}";
        var json = File.ReadAllText(jsonPath);
        // Making json object out of array
        json = "{\"cards\":" + json + "}";
        Root jsonObject = JsonUtility.FromJson<Root>(json);
        cards = jsonObject.cards;
    }

    [Serializable]
    private class Root {
        public List<CardData> cards;
    }
}
