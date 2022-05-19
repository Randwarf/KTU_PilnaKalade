using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearDeck : MonoBehaviour
{
    public void Clear()
    {
        PlayerDeck.Clear();
        if (PlayerPrefs.HasKey("Deck"))
        {
            PlayerPrefs.DeleteKey("Deck");
            PlayerPrefs.Save();
        }
    }
}
