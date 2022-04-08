using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour
{
    private Image[] tiles;
    private List<int> markedTiles;
    private List<int> placedTiles;

    void Start() {
        markedTiles = new List<int>();
        placedTiles = new List<int>();
        tiles = GetComponentsInChildren<Image>();
    }

    public void MarkTiles(List<int> indexes, Color color) {
        foreach (int index in indexes) {
            if (!placedTiles.Contains(index)) {
                tiles[index].color = color;
                markedTiles.Add(index);
            }
        }
    }

    public void UnmarkTiles() {
        foreach (int markedTile in markedTiles) {
            tiles[markedTile].color = Color.white;
        }
    }

    public void PlaceTiles(List<int> indexes, Color color) {
        markedTiles.Clear();
        foreach (int index in indexes) {
            tiles[index].color = color;
            placedTiles.Add(index);
        }
    }
}
