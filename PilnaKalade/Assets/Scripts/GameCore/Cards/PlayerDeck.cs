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
        for (int i = 0; i < 20; i++)
        {
            x = Random.Range(1, 3);
            deck.Add(CardDatabase.GetCard(x));
        }
    }
    public static List<CardData> GetDeck()
    {
        return deck;
    }

    // Update is called once per frame
    void Update()
    {

    }
}