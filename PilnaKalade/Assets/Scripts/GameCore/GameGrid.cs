using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour
{
    private Image[] tiles;
    private List<int> markedTiles;

    void Start() {
        markedTiles = new List<int>();
        tiles = GetComponentsInChildren<Image>();
    }

    public void MarkTiles(List<int> indexes, Color color) {
        foreach (int index in indexes) {
            tiles[index].color = color;
            markedTiles.Add(index);
        }
    }

    public void UnmarkTiles() {
        foreach (int markedTile in markedTiles) {
            tiles[markedTile].color = Color.white;
        }
    }
}
