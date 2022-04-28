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
        //We are technically loading the database twice.
        //once on main menu start and once when we enter battle scene.
        //Removing this load however, prevents us from directly starting the game
        //at the battle scene during the development
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