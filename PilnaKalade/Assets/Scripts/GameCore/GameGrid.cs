using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour
{
    private Image[] tiles;

    void Start()
    {
        tiles = GetComponentsInChildren<Image>();
    }

    public void MarkTiles(List<int> indexes, Color color) {
        foreach (int index in indexes) {
            tiles[index].color = color;
        }
    }
}
